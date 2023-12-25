using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AvaPMIS.Gateway.ApiResponseWrapper;
using AvaPMIS.Gateway.ApiResponseWrapper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;


namespace AvaPMIS.Gateway.ApiResponseWrapper
{
    public class ApiResponseWrapperMiddleware 
    {

#nullable disable
        private readonly IConfiguration _configuration;
        RequestDelegate _next;
        private ILogger<ApiResponseWrapperMiddleware> _logger;
        private readonly Func<object, Task> _clearCacheHeadersDelegate;
        public ApiResponseWrapperMiddleware(
            IConfiguration configuration,
          RequestDelegate next,
              ILogger<ApiResponseWrapperMiddleware> _logger)
        {
            _configuration = configuration;
            _next = next;
            this._logger = _logger;
            _clearCacheHeadersDelegate = ClearCacheHeaders;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ApiResponseWrapperMiddleware> logger)
        {
            _logger = logger;
            //_logger.LogWarning("Api log called!");
            try
            {

                var isEnabled = bool.Parse(GetConfigurationOrDefault("ApiResponseWrapper:IsEnabled", true));
                if (!isEnabled)
                {
                    await _next(httpContext);
                    return;
                }

                List<string> ignorePaths = null;
                var ignorePathConfigValue = GetConfigurationOrDefault<string>("ApiResponseWrapper:IgnorePaths", null);
                if (!string.IsNullOrEmpty(ignorePathConfigValue))
                {
                    ignorePaths = ignorePathConfigValue.Split(",").ToList();

                }

                if (ignorePaths != null && ignorePaths.Any())
                {
                    var mustIgnored = ignorePaths.Any(p =>
                        httpContext.Request.Path.StartsWithSegments((p.StartsWith("/") ? p : $"/{p}"), StringComparison.InvariantCultureIgnoreCase));
                    if (mustIgnored)
                    {
                        await _next(httpContext);
                        return;
                    }
                }
                var ignoreStartupConfigValue = GetConfigurationOrDefault<bool>("ApiResponseWrapper:IgnoreStartup", true);
                var ignoreStartupConfig = bool.Parse(ignoreStartupConfigValue);
                if (ignoreStartupConfig && (!httpContext.Request.Path.HasValue ||
                                            httpContext.Request.Path.Value == "" ||
                                            httpContext.Request.Path.Value == "/"))
                {

                    await _next(httpContext);
                    return;
                }
                var request = httpContext.Request;
                var stopWatch = Stopwatch.StartNew();
                var formattedRequest = await FormatRequest(request, 200);
                var originalBodyStream = httpContext.Response.Body;
                var requestTime = DateTime.UtcNow;
                using (var responseBody = new MemoryStream())
                {
                    httpContext.Response.Body = responseBody;

                    try
                    {
                        var response = httpContext.Response;
                        response.Body = responseBody;
                        await _next.Invoke(httpContext);

                        string responseBodyContent = null;
                        responseBodyContent = await FormatResponse(response);
                        var statusCode = (HttpStatusCode)httpContext.Response.StatusCode;
                         
                        if (new HttpResponseMessage(statusCode).IsSuccessStatusCode)
                        {
                            // responseBodyContent = await FormatResponse(response);


                            await HandleSuccessRequestAsync(httpContext, responseBodyContent, httpContext.Response.StatusCode);

                         }
                        else
                        {
                            await HandleNotSuccessRequestAsync(httpContext, responseBodyContent, httpContext.Response.StatusCode);
                        }

                        httpContext.Response.ContentLength = responseBody.Length;
                        stopWatch.Stop();

                    }
                    catch (Exception ex)
                    {

                        logger.LogError("An Inner Middleware exception occurred ", ex);
                        await HandleExceptionAsync(httpContext, ex);
                    }
                    finally
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }
            catch (Exception ex)
            {
                // We can't do anything if the response has already started, just abort.
                if (httpContext.Response.HasStarted)
                {
                    logger.LogWarning("A Middleware exception occurred, but response has already started!");
                    throw;
                }

                await HandleExceptionAsync(httpContext, ex);
                throw;
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _logger.LogError("Api Exception:", exception);

            ApiError apiError = null;
            ApiResponse apiResponse = null;
            var code = 0;
            if (exception is ApiException)
            {
                var ex = exception as ApiException;
                apiError = new ApiError(ResponseMessageEnum.ValidationError.GetDescription(), ex.Errors)
                {
                    ValidationErrors = ex.Errors,
                    ReferenceErrorCode = ex.ReferenceErrorCode,
                    ReferenceDocumentLink = ex.ReferenceDocumentLink
                };
                code = ex.StatusCode;
                httpContext.Response.StatusCode = code;

            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                code = StatusCodes.Status401Unauthorized;
                httpContext.Response.StatusCode = code;
            }
            else
            {
                var exceptionFlattened = exception.Flatten();
                //#if !DEBUG
                //                var msg = "An unhandled error occurred. please inform admin!";
                //                string stack = null;
                //#else
                var msg = exceptionFlattened;
                var stack = exception.StackTrace;
                //#endif

                apiError = new ApiError(msg)
                {
                    Details = stack
                };
                code = StatusCodes.Status500InternalServerError;
                httpContext.Response.StatusCode = code;
            }

            httpContext.Response.ContentType = "application/json";

            apiResponse = new ApiResponse(code, ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
        }

        private async Task HandleNotSuccessRequestAsync(HttpContext httpContext, string errorMessage, int code)
        {
            ApiError apiError;

            if (code == StatusCodes.Status404NotFound)
            {
                apiError = new ApiError(ResponseMessageEnum.NotFound.GetDescription());
            }
            else if (code == StatusCodes.Status204NoContent)
            {
                apiError = new ApiError(ResponseMessageEnum.NotContent.GetDescription());
            }
            else if (code == StatusCodes.Status405MethodNotAllowed)
            {
                apiError = new ApiError(ResponseMessageEnum.MethodNotAllowed.GetDescription());
            }
            else if (code == StatusCodes.Status401Unauthorized)
            {
                apiError = new ApiError(ResponseMessageEnum.UnAuthorized.GetDescription());
            }
            else if (code == StatusCodes.Status400BadRequest)
            {
                apiError = new ApiError(ResponseMessageEnum.ValidationError.GetDescription());
            }
            else if (code == 500)
            {
                apiError = new ApiError(ResponseMessageEnum.Exception.GetDescription());
                code = 400;
            }
            else
            {
                apiError = new ApiError(ResponseMessageEnum.Unknown.GetDescription());
            }
            var enablingErrorDetail = bool.Parse(GetConfigurationOrDefault("ApiResponseWrapper:EnablingErrorDetail", true));
            if (enablingErrorDetail)
            {
                apiError.Details = errorMessage;
                apiError.ExceptionMessage = errorMessage;
            }
            var apiResponse = new ApiResponse(code, apiError);

            httpContext.Response.StatusCode = code;
            httpContext.Response.ContentType = "application/json";
            var responseJson = ConvertToJSONString(apiResponse);
            var path = httpContext.Request.Path.ToString();
            //if request is save command we should log an event if an validation exception or format exception has been occured

            //httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            await httpContext.Response.WriteAsync(responseJson);
        }

        private Task HandleSuccessRequestAsync(HttpContext httpContext, object body, int code)
        {
            code = code == 201 ? 200 : code;
            var jsonString = string.Empty;
            var bodyText = !body.ToString().IsValidJson() ? ConvertToJSONString(body) : body.ToString();

            ApiResponse apiResponse = null;

            if (!body.ToString().IsValidJson())
            {
                return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
            }
            else
            {
                bodyText = body.ToString();
            }

            //TODO Review the code below as it might not be necessary
            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);
            Type type = bodyContent?.GetType();

            // Check to see if body is already an ApiResponse Class type
            if ((type.Equals(typeof(Newtonsoft.Json.Linq.JObject)) || type.Equals(typeof(Newtonsoft.Json.Linq.JArray))) && IsApiResponse(bodyContent))
            {

                apiResponse = JsonConvert.DeserializeObject<ApiResponse>(bodyText);
                if (apiResponse.StatusCode == 0)
                {
                    apiResponse.StatusCode = code;
                }

                if (apiResponse.Result != null || !string.IsNullOrEmpty(apiResponse.Message))
                {
                    jsonString = ConvertToJSONString(apiResponse);
                }
                else
                {
                    apiResponse = new ApiResponse(code, ResponseMessageEnum.Success.GetDescription(), bodyContent, null);
                    jsonString = ConvertToJSONString(apiResponse);
                }
            }
            else
            {
                apiResponse = new ApiResponse(code, ResponseMessageEnum.Success.GetDescription(), bodyContent, null);
                jsonString = ConvertToJSONString(apiResponse);
            }

            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(jsonString);
        }

        private bool IsApiResponse(object reponse)
        {
            if (reponse == null)
                return false;
            var properties = reponse.GetPublicProperties().Select(p => p.Name);
            var apiResponseProperties = new ApiResponse(200, "").GetPublicProperties().Select(p => p.Name);
            var diffs = apiResponseProperties.Except(properties).Count();
            if (diffs > 1)
                return false;
            else
            {
                return true;
            }
        }
        private async Task<string> FormatRequest(HttpRequest request, int? truncateLength = null)
        {
            //request.EnableBuffering();

            //var buffer = new byte[Convert.ToInt32(truncateLength.HasValue? truncateLength.Value: request.ContentLength)];
            //await request.Body.ReadAsync(buffer, 0, buffer.Length);
            //var bodyAsText = Encoding.UTF8.GetString(buffer);
            //request.Body.Seek(0, SeekOrigin.Begin);

            //return $"{request.Method} {request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
            return $"{request.Method} {request.Scheme} {request.Host}{request.Path} {request.QueryString}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return plainBodyText;
        }

        private string ConvertToJSONString(int code, object content)
        {
            return JsonConvert.SerializeObject(new ApiResponse(code, ResponseMessageEnum.Success.GetDescription(), content, null, string.Empty), JSONSettings());
        }
        private string ConvertToJSONString(ApiResponse apiResponse)
        {
            return JsonConvert.SerializeObject(apiResponse, JSONSettings());
        }
        private string ConvertToJSONString(object rawJSON)
        {
            return JsonConvert.SerializeObject(rawJSON, JSONSettings());
        }



        private JsonSerializerSettings JSONSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<Newtonsoft.Json.JsonConverter> { new StringEnumConverter() }
            };
        }



        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }


        public string GetConfigurationOrDefault<T>(string configurationName, T defaultValue = default)
        {
            if (_configuration[configurationName] != null)
                return _configuration[configurationName];
            else
            {
                return defaultValue == null ? null : defaultValue.ToString();
            }
        }
    }

}
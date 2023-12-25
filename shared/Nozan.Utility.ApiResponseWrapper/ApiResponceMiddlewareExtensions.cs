#nullable disable
using AvaPMIS;
using AvaPMIS.Gateway.AutoWrapper;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;

namespace AvaPMIS.Gateway.ApiResponseWrapper
{
    public static class ApiResponceMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResponseMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<ApiResponseWrapperMiddleware>();
        }
    }
}
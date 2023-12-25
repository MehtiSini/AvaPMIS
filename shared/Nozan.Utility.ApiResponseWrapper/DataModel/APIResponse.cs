using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AvaPMIS.Gateway.ApiResponseWrapper
{
    [Serializable]
    [DataContract]
    public class ApiResponse : ApiResponse<object>
    {
        [JsonConstructor]
        public ApiResponse(int statusCode, string message = "", object result = default, ApiError apiError = null, string apiVersion = "") : base(statusCode, message, result, apiError, apiVersion)
        {
        }

        public ApiResponse(int statusCode, ApiError apiError) : base(statusCode, apiError)
        {
            this.Result = apiError;
        }
    }
    [Serializable]
    [DataContract]
    public class ApiResponse<T>
    {
        /// <summary>
        /// نسخه سرویس
        /// </summary>
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// وضعیت فراخوانی سرویس
        /// </summary>
        [DataMember]
        public int StatusCode { get; set; }

        /// <summary>
        /// ایا فراخوانی سرویس با خطا مواجه شد؟
        /// </summary>
        [DataMember]
        public bool IsError { get; set; }

        /// <summary>
        /// پیام/توضیحات
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// خطا
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ApiError ResponseException { get; set; }

        /// <summary>
        /// خروجی سرویس
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }


        public ApiResponse()
        { }
        [JsonConstructor]
        public ApiResponse(int statusCode, string message = "", T result = default, ApiError apiError = null, string apiVersion = "")
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            ResponseException = apiError;
            Version = string.IsNullOrWhiteSpace(apiVersion) ? Assembly.GetEntryAssembly().GetName().Version.ToString() : apiVersion;
            IsError = false;
        }

        public ApiResponse(int statusCode, ApiError apiError)
        {
            StatusCode = statusCode;
            Message = apiError.ExceptionMessage;
            ResponseException = apiError;
            IsError = true;
        }
    }
}

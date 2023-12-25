#nullable disable
using AvaPMIS;

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
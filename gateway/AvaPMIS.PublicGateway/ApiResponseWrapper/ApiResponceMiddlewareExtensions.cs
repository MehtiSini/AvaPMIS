#nullable disable
namespace AvaPMIS.PublicGateway.ApiResponseWrapper
{
    public static class ApiResponceMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseWrapperMiddleware>();
        }
    }
}
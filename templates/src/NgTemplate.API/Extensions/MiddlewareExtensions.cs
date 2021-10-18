using Microsoft.AspNetCore.Builder;
using NgTemplate.API.Middleware;

namespace NgTemplate.API.Extensions
{
    public static class MiddlewareExtensions
    {
         public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
using Microsoft.AspNetCore.Builder;

namespace Product.Api.AppConfigs
{
    public static  class ExceptionMiddlewareExtensions {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
    
}

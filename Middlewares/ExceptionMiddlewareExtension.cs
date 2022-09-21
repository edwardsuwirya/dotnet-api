using Microsoft.AspNetCore.Diagnostics;

namespace MySimpleNetApi.Middlewares;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Console.WriteLine($"{contextFeature.Error.Message}");
                    await context.Response.WriteAsync("Internal Server Error");
                }
            });
        });
    }
}
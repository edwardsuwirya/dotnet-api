using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using MySimpleNetApi.Utils;

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
                    context.Response.StatusCode = (int)GetErrorCode(contextFeature.Error);
                    Console.WriteLine($"{contextFeature.Error.Message}");
                    await context.Response.WriteAsJsonAsync(
                        new CommonResponse<string>(context.Response.StatusCode.ToString(),
                            $"{contextFeature.Error.Message}")
                    );
                }
            });
        });
    }

    private static HttpStatusCode GetErrorCode(Exception e)
    {
        switch (e)
        {
            case ValidationException _:
                return HttpStatusCode.BadRequest;
            case FormatException _:
                return HttpStatusCode.BadRequest;
            case AuthenticationException _:
                return HttpStatusCode.Forbidden;
            default:
                return HttpStatusCode.InternalServerError;
        }
    }
}
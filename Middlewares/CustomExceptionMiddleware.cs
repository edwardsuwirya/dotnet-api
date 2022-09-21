using System.Net;

namespace MySimpleNetApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public CustomExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Console.WriteLine($"{exception.Message}");
        await context.Response.WriteAsync("Internal Server Error");
    }
}
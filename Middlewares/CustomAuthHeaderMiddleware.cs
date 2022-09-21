using System.Net;

namespace MySimpleNetApi.Middlewares;

public class CustomAuthHeaderMiddleware
{
    private readonly RequestDelegate _next;
    public CustomAuthHeaderMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"];
        if (authHeader == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }
        else
        {
            await _next(context);
        }
    }
}
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Utils;

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
        var statusCode = "0";
        switch (exception)
        {
            case BadRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                statusCode = "01";
                break;
            case NotFoundException:
                statusCode = "02";
                break;
            case UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                statusCode = "04";
                break;
            case DbException:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                statusCode = "05";
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                statusCode = "06";
                break;
        }

        Console.WriteLine($"{exception.Message}");
        var jsonOpt = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        await context.Response.WriteAsJsonAsync(new CommonResponse<string>(statusCode, $"{exception.Message}")
            , jsonOpt);
    }
}
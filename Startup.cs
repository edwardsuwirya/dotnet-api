using System.Text.Json.Serialization;
using MySimpleNetApi.Middlewares;

namespace MySimpleNetApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    // ConfigureServices akan di-call oleh framework secara otomatis
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
    }


    // Configure method akan di-call oleh framework secara otomatis
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Create middleware using extension method
        // app.ConfigureExceptionHandler();

        // Create custom middlware
        app.UseMiddleware<CustomExceptionMiddleware>();
        app.UseMiddleware<CustomAuthHeaderMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.Use(async (context, next) =>
        {
            Console.WriteLine("Hello enigma from middleware");
            Console.WriteLine(context.Request.Path);
            Console.WriteLine(context.Request.Host);
            await next();
            Console.WriteLine(context.Response.StatusCode);
        });

        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello Enigma!"); });
            endpoints.MapControllers();
        });
    }
}
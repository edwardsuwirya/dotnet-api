using MySimpleNetApi.Authentication;
using MySimpleNetApi.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MySimpleNetApi.Middlewares;
using MySimpleNetApi.Repository;

namespace MySimpleNetApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    // ConfigureServices akan di-call oleh framework secara otomatis
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication("Custom").AddScheme<CustomAuthOptions, CustomAuthHandler>("Custom", null);
        services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
        );
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase(_configuration.GetConnectionString("memory"));
        });
        services.AddTransient<IPersistence, DbPersistence>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICategoryService, CategoryService>();
    }


    // Configure method akan di-call oleh framework secara otomatis
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Create middleware using extension method
        // app.ConfigureExceptionHandler();

        // Create custom middlware
        app.UseMiddleware<CustomExceptionMiddleware>();
        // app.UseMiddleware<CustomAuthHeaderMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
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
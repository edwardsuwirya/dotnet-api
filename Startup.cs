namespace MySimpleNetApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    // ConfigureServices akan di-call oleh framework secara otomatis
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }


    // Configure method akan di-call oleh framework secara otomatis
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello Enigma!"); });
            endpoints.MapControllers();
        });
    }
}
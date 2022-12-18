namespace GuestBook;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
    }
 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting()
            .UseSwagger()
            .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"))
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
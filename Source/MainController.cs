using Serilog;
using Serilog.Events;
using Serial;

public class MainController
{
    public MainController()
    {
        
    }

    public void RunApplication(string[] commandLineArgs)
    {
        DotNetEnv.Env.Load();
        var builder = WebApplication.CreateBuilder(commandLineArgs);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHostedService<SketchController>();
        builder.Services.AddSingleton<Services.IImageUploadService, Services.ImageUploadService>();
        builder.Services.AddSingleton<Services.ILineMappingService, Services.LineMappingService>();
        builder.Services.AddSingleton<Services.IActionMappingService, Services.ActionMappingService>();
        if(Environment.GetEnvironmentVariable("CONTROL_GENERATION") == "serial")
        {
            builder.Services.AddSingleton<IControlConverter, SerialControlConverter>();
        }
        else
        {
            builder.Services.AddSingleton<IControlConverter, VirtualControlConverter>();
        }

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapControllers();
        
        Log.Information($"Application launching at {Environment.GetEnvironmentVariable("ASPNETCORE_URLS")}");
        app.Run();

    }
}
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
        var builder = WebApplication.CreateBuilder(commandLineArgs);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHostedService<SketchController>();

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

        app.MapControllers();
        
        Log.Information($"Application launching at {Environment.GetEnvironmentVariable("ASPNETCORE_URLS")}");
        app.Run();

    }
    private void RunImageApi()
    {
        
    }
}
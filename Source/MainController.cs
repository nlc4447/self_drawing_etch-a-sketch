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

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
    private void RunImageApi()
    {
        
    }
}
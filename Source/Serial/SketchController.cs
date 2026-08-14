namespace Serial;

// Runs concurrently with the web host so serial/drawing work doesn't block API requests.
public class SketchController : BackgroundService
{
    private readonly ILogger<SketchController> _logger;

    public SketchController(ILogger<SketchController> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // TODO: poll for pending sketch work and send it over serial.
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}

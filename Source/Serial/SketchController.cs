namespace Serial;
using Models;
using Services;

// Runs concurrently with the web host so serial/drawing work doesn't block API requests.
public class SketchController : BackgroundService
{
    private readonly ILogger<SketchController> _logger;
    private readonly IControlConverter _controlConverter;

    public SketchController(IControlConverter controlConverter, IActionMappingService actionMappingService, ILogger<SketchController> logger)
    {
        ArgumentNullException.ThrowIfNull(controlConverter);
        ArgumentNullException.ThrowIfNull(actionMappingService);
        ArgumentNullException.ThrowIfNull(logger);

        _controlConverter = controlConverter;
        _logger = logger;

        actionMappingService.MappingReady += OnActionMapReady;
    }

    private void OnActionMapReady(object? sender, ActionMap actionMap)
    {
        _controlConverter.DrawCircle();
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

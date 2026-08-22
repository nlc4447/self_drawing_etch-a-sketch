namespace Services;
using Models;

public class ActionMappingService : IActionMappingService
{
    public event EventHandler<ActionMap>? MappingReady;

    public ActionMappingService(ILineMappingService lineMappingService)
    {
        ArgumentNullException.ThrowIfNull(lineMappingService);

        lineMappingService.MappingReady += OnLineMapReady;
    }

    public ActionMap GetMapping()
    {
        // Placeholder implementation
        return new ActionMap();
    }

    private void OnLineMapReady(object? sender, LineMap lineMap)
    {
        MappingReady?.Invoke(this, GetMapping());
    }
}
namespace Services;
using Models;

public interface IActionMappingService
{
    ActionMap GetMapping();

    // Signals downstream stages that a new action map is ready to be drawn.
    event EventHandler<ActionMap>? MappingReady;
}
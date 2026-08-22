namespace Services;
using Models;

public interface ILineMappingService
{
    public LineMap GetMapping();

    // Signals downstream stages that a new line map is ready to be converted into actions.
    event EventHandler<LineMap>? MappingReady;
}
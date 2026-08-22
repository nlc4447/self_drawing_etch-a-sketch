namespace Services;
using Models;

public class LineMappingService : ILineMappingService
{
    public event EventHandler<LineMap>? MappingReady;

    public LineMappingService(IImageUploadService imageUploadService)
    {
        ArgumentNullException.ThrowIfNull(imageUploadService);

        imageUploadService.Uploaded += OnUploaded;
    }

    public LineMap GetMapping()
    {
        // Placeholder implementation
        return new LineMap();
    }

    private void OnUploaded(object? sender, EventArgs e)
    {
        MappingReady?.Invoke(this, GetMapping());
    }
}
namespace Services;
using Models;

public interface IImageUploadService
{
    public string Upload(IFormFile file);

    // Signals downstream stages that a new image is ready to be mapped into lines.
    event EventHandler? Uploaded;
}
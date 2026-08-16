namespace Services;
using Models;

public interface IImageUploadService
{
    public string Upload(IFormFile file);
}
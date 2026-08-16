using Avalonia.Controls;

namespace Services;

public class ImageUploadService : IImageUploadService
{
    private readonly ILogger<ImageUploadService> _logger;
    private readonly string _imageFileName = "latest.png";
    private readonly string _uploadDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

    public ImageUploadService(ILogger<ImageUploadService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    public string Upload(IFormFile file)
    {
        _logger.LogInformation("Starting image upload process.");
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        // Validate file format
        var extensions = new [] {".png", ".jpg", ".jpeg", ".heic"};
        if (!extensions.Contains(fileExtension))
        {
            throw new ArgumentException($"Invalid file format. Only PNG, JPG, JPEG, and HEIC files are allowed. Detected format: {fileExtension}");
        }

        // Validate file size
        int fileSizeLimit = 5 * 1024 * 1024; // 5 MB
        if (file.Length > fileSizeLimit)
        {
            throw new ArgumentException("File size exceeds the limit of 5 MB.");
        }
        else if (file.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }

        File.Delete(Path.Combine(_uploadDirectoryPath, _imageFileName));

        // Change the name of the file to a common name
        using (FileStream stream = new(Path.Combine(_uploadDirectoryPath, _imageFileName), FileMode.Create))
        {
            file.CopyTo(stream);
        }
        _logger.LogInformation($"Image uploaded successfully as {_imageFileName}.");
        return _imageFileName;
    }
}
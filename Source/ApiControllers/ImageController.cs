using Microsoft.AspNetCore.Mvc;
using Services;

namespace ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;
    private readonly IImageUploadService _imageUploadService;

    public ImageController(ILogger<ImageController> logger, IImageUploadService imageUploadService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(imageUploadService);

        _logger = logger;
        _imageUploadService = imageUploadService;
    }
    
    [HttpPost]
    public IActionResult UploadImage(IFormFile file)
    {
        //TODO: represent as webpage so user can upload image from browser
        _logger.LogInformation($"Received image upload request for file: {file.FileName}, size: {file.Length / 1024} KB.");
        try
        {
            _imageUploadService.Upload(file);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Invalid image file received: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
        _logger.LogInformation("Image upload request processed successfully.");
        return Ok();
    }
}
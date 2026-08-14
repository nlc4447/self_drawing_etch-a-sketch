using Microsoft.AspNetCore.Mvc;

namespace ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;
    
    public ImageController(ILogger<ImageController> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }
    
    [HttpPost("{id:guid}")]
    public IActionResult GetImage(Guid id)
    {
        _logger.LogInformation("Received request to retrieve image with ID: {ImageId}", id);
        return NotFound(new { id, message = "Image retrieval is not yet implemented." });
    }
}
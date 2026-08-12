using Microsoft.AspNetCore.Mvc;

namespace ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    
    [HttpPost("{id:guid}")]
    public IActionResult GetImage(Guid id)
    {
        return NotFound(new { id, message = "Image retrieval is not yet implemented." });
    }
}
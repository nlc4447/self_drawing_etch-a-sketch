

using Microsoft.AspNetCore.Mvc;

namespace ApiControllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStatus()
    {
        return Ok(new { status = "Image API ready" });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetImage(Guid id)
    {
        return NotFound(new { id, message = "Image retrieval is not implemented yet." });
    }

    [HttpPost]
    public IActionResult CreateImage([FromBody] object imageRequest)
    {
        var newId = Guid.NewGuid();
        return CreatedAtAction(nameof(GetImage), new { id = newId }, new { id = newId, message = "Image creation placeholder." });
    }
}
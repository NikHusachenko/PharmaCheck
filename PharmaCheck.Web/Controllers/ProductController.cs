using Microsoft.AspNetCore.Mvc;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return Ok();
    }
}
using Microsoft.AspNetCore.Mvc;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> Create() => Ok();

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll() => Ok(); // TODO Filter model

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) => Ok();

    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchByName([FromRoute] string name) => Ok();

    [HttpPost]
    public async Task<IActionResult> AttachToSupply() => Ok();

    // new
    // get/all
    // get/{id}
    // search/{name}
    // attach-to-supply/{id}
}
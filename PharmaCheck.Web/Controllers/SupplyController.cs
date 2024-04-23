using Microsoft.AspNetCore.Mvc;

namespace PharmaCheck.Web.Controllers;

[Route("api/supplier/{supplierId:guid}/supply")]
[ApiController]
public class SupplyController : ControllerBase
{
    // TODO
    [HttpPost("new")]
    public async Task<IActionResult> NewSupply([FromRoute] Guid supplierId) => Ok();

    // TODO
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromRoute] Guid supplierId) => Ok();
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.Supply.GetSupplies;
using PharmaCheck.Domain.Supply.NewSupply;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;
using PharmaCheck.Web.Infrastructure;

namespace PharmaCheck.Web.Controllers;

[Route("api/supplier/{supplierId:guid}/supply")]
[ApiController]
public class SupplyController(IMediator mediator) : ControllerBase
{
    // TODO
    [HttpPost("new")]
    public async Task<IActionResult> NewSupply([FromRoute] Guid supplierId) => 
        await mediator.Send(new NewSupplyRequest(supplierId))
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    // TODO
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromRoute] Guid supplierId) =>
        await mediator.Send(new GetSuppliesRequest(supplierId))
            .Map<List<SupplyModel>, IActionResult>(list => list.Any() ? Ok(list) : NoContent());
}
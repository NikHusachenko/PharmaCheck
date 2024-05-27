using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.Supply.GetSupplies;
using PharmaCheck.Domain.Supply.NewSupply;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Domain.Supply.GetSupplyById;

namespace PharmaCheck.Web.Controllers;

[Route("api/supply")]
[ApiController]
public class SupplyController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> NewSupply([FromBody] NewSupplyRequest request) => 
        await mediator.Send(request).Map<Result<Guid>, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromBody] GetSuppliesRequest request) =>
        await mediator.Send(request).Map<List<SupplyModel>, IActionResult>(
            list => list.Any() ? Ok(list) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetSupplyByIdRequest request) =>
        await mediator.Send(request).Map(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            Ok(result.Value));
}
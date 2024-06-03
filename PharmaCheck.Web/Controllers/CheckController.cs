using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Check.GetById;
using PharmaCheck.Domain.Check.New;
using PharmaCheck.Domain.Check.Pay;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Services.Response;
using PharmaCheck.Web.Infrastructure;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckController(IMediator mediator) : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> Create([FromBody] NewCheckRequest request) =>
        await mediator.Send(request).Map<Result<CheckEntity>, IActionResult>(
            result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        await mediator.Send(new GetCheckByIdRequest(id)).Map<Result<CheckModel>, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            Ok(result.Value));

    [HttpPost("pay/{id:guid}")]
    public async Task<IActionResult> Pay([FromRoute] Guid id) =>
        await mediator.Send(new PayCheckRequest(id)).Map<Result, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            NoContent());
}
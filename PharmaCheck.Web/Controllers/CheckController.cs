using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Check.New;
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
}
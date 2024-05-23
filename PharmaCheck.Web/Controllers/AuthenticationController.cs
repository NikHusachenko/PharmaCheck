using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.User.SignUp;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Services.Response;
using PharmaCheck.Web.Infrastructure;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request, CancellationToken cancellationToken = default) =>
        await mediator.Send(request, cancellationToken)
            .Map<Result<string>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));
}
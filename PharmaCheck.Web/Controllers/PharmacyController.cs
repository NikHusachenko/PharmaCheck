using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.Pharmacy.GetPharmacies;
using PharmaCheck.Domain.Pharmacy.NewPharmacy;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PharmacyController(IMediator mediator) : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> CreateNew([FromBody] NewPharmacyRequest request) =>
        await mediator.Send(request)
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetPharmaciesRequest request) =>
        await mediator.Send(request)
            .Map<List<PharmacyModel>, IActionResult>(result =>
                result.Any() ? Ok(result) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById() => Ok();

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName() => Ok();
}
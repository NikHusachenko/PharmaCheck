using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.Pharmacy.GetPharmacies;
using PharmaCheck.Domain.Pharmacy.GetPharmaciesByProductId;
using PharmaCheck.Domain.Pharmacy.GetPharmacyById;
using PharmaCheck.Domain.Pharmacy.GetPharmacyByName;
using PharmaCheck.Domain.Pharmacy.NewPharmacy;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Web.Infrastructure;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PharmacyController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateNew([FromBody] NewPharmacyRequest request) =>
        await mediator.Send(request)
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetPharmaciesRequest request) =>
        await mediator.Send(request)
            .Map<List<PharmacyModel>, IActionResult>(result =>
                result.Any() ? Ok(result) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        await mediator.Send(new GetPharmacyByIdRequest(id))
            .Map<Result<PharmacyModel>, IActionResult>(result => result.IsError ?
                NotFound(ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        await mediator.Send(new GetPharmacyByNameRequest(name))
            .Map<Result<PharmacyModel>, IActionResult>(result => result.IsError ?
                NotFound(ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/product/{id:guid}")]
    public async Task<IActionResult> GetByProductId([FromRoute] Guid id) =>
        await mediator.Send(new GetPharmaciesByProductIdRequest(id))
            .Map<List<PharmacyModel>, IActionResult>(result => result.Any() ? Ok(result) : NoContent());
}
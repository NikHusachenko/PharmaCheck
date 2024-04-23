using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.Supplier.CreateSupplier;
using PharmaCheck.Domain.Supplier.GetSuppliers;
using PharmaCheck.Utilities.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.Supplier;

namespace PharmaCheck.Web.Controllers;

[Route("api/supplier")]
[ApiController]
public class SupplierController(IMediator mediator) : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> New([FromBody] NewSupplierHttpPostModel model) =>
        await mediator.Send(new CreateSupplierRequest(
            model.Name,
            model.Region,
            model.City,
            model.Street,
            model.AdditionAddress,
            model.ContactPhone))
            .Map(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetSuppliersFilter filter) =>
        await mediator.Send(new GetSuppliersRequest(filter.Page,
            filter.NameQuery,
            filter.RegionQuery,
            filter.CityQuery,
            filter.StreetQuery,
            filter.AdditionAddressQuery,
            filter.ContactPhoneQuery))
            .Map<List<SupplierModel>, IActionResult>(list => list.Any() ? Ok(list) : NoContent());

    // TODO
    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromQuery] Guid id) => Ok();
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.ProductType.CreateProductType;
using PharmaCheck.Domain.ProductType.GetProductTypeById;
using PharmaCheck.Domain.ProductType.GetProductTypeByName;
using PharmaCheck.Domain.ProductType.GetProductTypes;
using PharmaCheck.Domain.ProductType.Models;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.ProductType;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductTypeController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductTypeHttpPostModel model) =>
        await mediator.Send(new CreateProductTypeRequest(model.CategoryId, model.Name))
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetProductTypesFilter filter) =>
        await mediator.Send(new GetProductTypesRequest(filter.Page, filter.NameQuery, filter.CategoryId))
            .Map<IEnumerable<ProductTypeModel>, IActionResult>(response => 
                response.Any() ? Ok(response) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        await mediator.Send(new GetProductTypeByIdRequest(id))
            .Map(result => result.IsError ?
                NotFound(ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        await mediator.Send(new GetProductTypeByNameRequest(name))
            .Map(result => result.IsError ?
                NotFound(ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));
}
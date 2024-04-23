using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.ProductType.CreateProductType;
using PharmaCheck.Domain.ProductType.GetProductTypeById;
using PharmaCheck.Domain.ProductType.GetProductTypeByName;
using PharmaCheck.Domain.ProductType.GetProductTypes;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.ProductType;

namespace PharmaCheck.Web.Controllers;

[Route("api/category/{categoryId:Guid}/product-type")]
[ApiController]
public sealed class ProductTypeController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid categoryId, [FromBody] CreateProductTypeHttpPostModel model) =>
        await mediator.Send(new CreateProductTypeRequest(categoryId, model.Name))
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromRoute] Guid categoryId, [FromQuery] GetProductTypesFilter filter) =>
        await mediator.Send(new GetProductTypesRequest(filter.Page, filter.NameQuery, categoryId))
            .Map<IEnumerable<ProductTypeModel>, IActionResult>(response => 
                response.Any() ? Ok(response) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid categoryId, [FromRoute] Guid id) =>
        await mediator.Send(new GetProductTypeByIdRequest(categoryId, id))
            .Map<Result<ProductTypeModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] Guid categoryId, [FromRoute] string name) =>
        await mediator.Send(new GetProductTypeByNameRequest(categoryId, name))
            .Map<Result<ProductTypeModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Models;
using PharmaCheck.Domain.ProductType.CreateProductType;
using PharmaCheck.Domain.ProductType.GetProductTypeById;
using PharmaCheck.Domain.ProductType.GetProductTypeByName;
using PharmaCheck.Domain.ProductType.GetProductTypes;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.CategoryType;

namespace PharmaCheck.Web.Controllers;

[Route("api/product-type")]
[ApiController]
public sealed class ProductTypeController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductTypeRequest request) =>
        await mediator.Send(request)
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetProductTypesRequest filter) =>
        await mediator.Send(filter)
            .Map<IEnumerable<ProductTypeModel>, IActionResult>(response =>
                response.Any() ? Ok(response) : NoContent());

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById(GetProductTypeByIdRequest request) =>
        await mediator.Send(request)
            .Map<Result<ProductTypeModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        await mediator.Send(new GetProductTypeByNameRequest(name))
            .Map<Result<ProductTypeModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));
}
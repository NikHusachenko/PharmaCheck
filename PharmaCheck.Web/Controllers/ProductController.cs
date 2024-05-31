using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Product.AttachToCheck;
using PharmaCheck.Domain.Product.Get;
using PharmaCheck.Domain.Product.NewProduct;
using PharmaCheck.Domain.Product.RemoveFromCheck;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Services.Response;
using PharmaCheck.Web.Infrastructure;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] NewProductRequest request) =>
        await mediator.Send(request).Map<Result<ProductEntity>, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsRequest request) =>
        await mediator.Send(request).Map<List<ProductEntity>, IActionResult>(
            list => list.Any() ? Ok(list) : NoContent());

    [HttpPost("check/attach")]
    public async Task<IActionResult> AttachToCheck([FromBody] AttachToCheckRequest request) =>
        await mediator.Send(request).Map<Result, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            NoContent());

    [HttpPost("check/detach")]
    public async Task<IActionResult> DetachFromCheck([FromBody] RemoveFromCheckRequest request) =>
        await mediator.Send(request).Map<Result, IActionResult>(
            result => result.IsError ?
            StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
            NoContent());
}
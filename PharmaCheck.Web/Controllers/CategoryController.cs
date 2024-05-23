using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Category.CreateCategory;
using PharmaCheck.Domain.Category.GetCategories;
using PharmaCheck.Domain.Category.GetCategoryById;
using PharmaCheck.Domain.Category.GetCategoryByName;
using PharmaCheck.Domain.Category.UpdateCategory;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.CategoryType;

namespace PharmaCheck.Web.Controllers;

[Route("api/category")]
[ApiController]
public sealed class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request) =>
        await mediator.Send(request)
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> Get([FromQuery] GetCategoriesRequest request) =>
        await mediator.Send(request)
            .Map<IEnumerable<CategoryModel>, IActionResult>(response => 
                response.Any() ? Ok(response) : NoContent());

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        await mediator.Send(new GetCategoryByNameRequest(name))
            .Map<Result<CategoryModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        await mediator.Send(new GetCategoryByIdRequest(id))
            .Map<Result<CategoryModel>, IActionResult>(result => 
                result.IsError ? NoContent() : Ok(result.Value));

    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryNameModel model) =>
        await mediator.Send(new UpdateCategoryRequest(id, model.NewName))
            .Map<Result, IActionResult>(response => response.IsError ?
                StatusCode((int)response.StatusCode, ControllerResponse.ToErrorResult(response.ErrorMessage)) :
                NoContent());
}
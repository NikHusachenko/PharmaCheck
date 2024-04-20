using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Category.CreateCategory;
using PharmaCheck.Domain.Category.GetCategories;
using PharmaCheck.Domain.Category.GetCategoryById;
using PharmaCheck.Domain.Category.GetCategoryByName;
using PharmaCheck.Domain.Category.Models;
using PharmaCheck.Domain.Category.UpdateCategory;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;
using PharmaCheck.Web.Infrastructure;
using PharmaCheck.Web.Models.Category;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    IMediator mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel model) =>
        await mediator.Send(new CreateCategoryRequest(model.Name))
            .Map<Result<Guid>, IActionResult>(result => result.IsError ?
                BadRequest(ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                Ok(result.Value));

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetCategoriesFilterModel filter) =>
        await mediator.Send(new GetCategoriesRequest(filter.NameQuery))
            .Map<IEnumerable<CategoryModel>, IActionResult>(response => 
                response.Any() ? Ok(response) : NotFound());

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        await mediator.Send(new GetCategoryByNameRequest(name))
            .Map(result => result.IsError ? 
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) : 
                StatusCode((int)result.StatusCode, result.Value));

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) =>
        await mediator.Send(new GetCategoryByIdRequest(id))
            .Map<Result<CategoryModel>, IActionResult>(result => result.IsError ?
                StatusCode((int)result.StatusCode, ControllerResponse.ToErrorResult(result.ErrorMessage)) :
                StatusCode((int)result.StatusCode, result.Value));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] string name) =>
        await mediator.Send(new UpdateCategoryRequest(id, name))
            .Map<Result, IActionResult>(response => response.IsError ?
                NoContent() :
                StatusCode((int)response.StatusCode, ControllerResponse.ToErrorResult(response.ErrorMessage)));
}
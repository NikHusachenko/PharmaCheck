using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmaCheck.Domain.Category.GetCategories;
using PharmaCheck.Web.Models.Category;

namespace PharmaCheck.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    IMediator mediator)
    : ControllerBase
{
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll([FromQuery] GetCategoriesFilterModel filter) =>
        Ok(await mediator.Send(new GetCategoriesRequest()
        {
            Query = filter.NameQuery,
        }));

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        return Ok();
    }

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        return Ok();
    }
}
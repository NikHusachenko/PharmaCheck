using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed record GetCategoriesRequest : IRequest<IEnumerable<CategoryModel>>
{
    public int Page { get; set; }
    public string Query { get; set; } = string.Empty;
}
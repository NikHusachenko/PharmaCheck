using MediatR;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed record GetCategoriesRequest : IRequest<IEnumerable<CategoryModel>>
{
    public string Query { get; set; } = string.Empty;
}
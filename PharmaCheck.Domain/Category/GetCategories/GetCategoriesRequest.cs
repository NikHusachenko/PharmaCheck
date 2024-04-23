using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed record GetCategoriesRequest(int Page, string Query) : IRequest<IEnumerable<CategoryModel>>;
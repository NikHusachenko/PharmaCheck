using MediatR;
using PharmaCheck.Domain.Category.Models;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed record GetCategoriesRequest(string Query) : IRequest<IEnumerable<CategoryModel>>;
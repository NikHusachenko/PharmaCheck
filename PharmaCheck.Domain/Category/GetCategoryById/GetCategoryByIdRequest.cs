using MediatR;
using PharmaCheck.Domain.Category.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.GetCategoryById;

public sealed record GetCategoryByIdRequest(Guid Id) : IRequest<Result<CategoryModel>>;
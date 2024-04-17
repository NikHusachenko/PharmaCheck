using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.CreateCategory;

public sealed record CreateCategoryRequest(string Name) : IRequest<Result<Guid>>;
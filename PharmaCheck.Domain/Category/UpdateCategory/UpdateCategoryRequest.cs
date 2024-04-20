using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.UpdateCategory;

public sealed record UpdateCategoryRequest(Guid Id, string NewName) : IRequest<Result>;
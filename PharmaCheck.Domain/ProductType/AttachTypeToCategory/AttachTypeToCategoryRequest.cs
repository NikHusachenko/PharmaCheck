using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.AttachTypeToCategory;

public sealed record AttachTypeToCategoryRequest(Guid TypeId, Guid CategoryId) : IRequest<Result>;
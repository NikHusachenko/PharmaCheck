using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.UpdateProductTypeName;

public sealed record UpdateProductTypeNameRequest(Guid CategoryId, Guid Id, string NewName) : IRequest<Result>;
using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.UpdateProductTypeName;

public sealed record UpdateProductTypeNameRequest(Guid Id, string NewName) : IRequest<Result>;
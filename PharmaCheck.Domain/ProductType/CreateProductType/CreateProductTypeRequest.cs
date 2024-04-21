using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.CreateProductType;

public sealed record CreateProductTypeRequest(Guid CategoryId, string Name) : IRequest<Result<Guid>>;
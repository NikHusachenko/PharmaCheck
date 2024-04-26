using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.NewProduct;

public sealed record NewProductRequest(string Name,
    string Description,
    string Manufacturer,
    float Price) : IRequest<Result<Guid>>;
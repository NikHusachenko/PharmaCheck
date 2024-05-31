using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.NewProduct;

public sealed record NewProductRequest(string Name,
    string Description,
    string Manufacturer,
    float Price,
    Guid ProductTypeId) : IRequest<Result<ProductEntity>>;
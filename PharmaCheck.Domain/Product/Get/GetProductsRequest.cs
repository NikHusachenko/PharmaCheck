using MediatR;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.Domain.Product.Get;

public sealed record GetProductsRequest(Guid? PharmacyId, string? Name, string? Description, string? Manufacturer, float? MinPrice, float? MaxPrice, int Page = 1) : IRequest<List<ProductEntity>>;
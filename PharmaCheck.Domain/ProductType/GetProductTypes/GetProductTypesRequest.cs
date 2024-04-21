using MediatR;
using PharmaCheck.Domain.ProductType.Models;

namespace PharmaCheck.Domain.ProductType.GetProductTypes;

public sealed record GetProductTypesRequest(int Page, string QueryName, Guid CategoryId) : IRequest<IEnumerable<ProductTypeModel>>;
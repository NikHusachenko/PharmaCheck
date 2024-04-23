using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Supply.GetSupplies;

public sealed record GetSuppliesRequest(Guid SupplierId) : IRequest<List<SupplyModel>>;
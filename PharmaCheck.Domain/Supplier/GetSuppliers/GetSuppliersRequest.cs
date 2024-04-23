using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Supplier.GetSuppliers;

public sealed record GetSuppliersRequest(
    int Page,
    string NameQuery,
    string RegionQuery,
    string CityQuery,
    string StreetQuery,
    string AdditionAddressQuery,
    string ContactPhoneQuery)
    : IRequest<List<SupplierModel>>;
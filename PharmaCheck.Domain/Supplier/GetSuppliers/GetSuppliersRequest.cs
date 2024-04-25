using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Supplier.GetSuppliers;

public sealed record GetSuppliersRequest : IRequest<List<SupplierModel>>
{
    public int Page { get; set; }
    public string NameQuery { get; set; } = string.Empty;
    public string RegionQuery { get; set; } = string.Empty;
    public string CityQuery { get; set; } = string.Empty;
    public string StreetQuery { get; set; } = string.Empty;
    public string AdditionAddressQuery { get; set; } = string.Empty;
    public string ContactPhoneQuery { get; set; } = string.Empty;
}
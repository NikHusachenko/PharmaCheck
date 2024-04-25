using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacies;

public sealed record GetPharmaciesRequest : IRequest<List<PharmacyModel>>
{
    public string CityQuery { get; set; } = string.Empty;
    public string RegionQuery { get; set; } = string.Empty;
    public string StreetQuery { get; set; } = string.Empty;
    public string AdditionAddressQuery { get; set; } = string.Empty;
}
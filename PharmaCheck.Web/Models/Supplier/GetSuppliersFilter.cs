namespace PharmaCheck.Web.Models.Supplier;

public sealed record GetSuppliersFilter
{
    public int Page { get; set; }
    public string NameQuery { get; set; } = string.Empty;
    public string RegionQuery { get; set; } = string.Empty;
    public string CityQuery { get; set; } = string.Empty;
    public string StreetQuery { get; set; } = string.Empty;
    public string AdditionAddressQuery { get; set; } = string.Empty; 
    public string ContactPhoneQuery { get; set; } = string.Empty;
}

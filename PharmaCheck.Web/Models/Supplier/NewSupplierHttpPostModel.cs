using PharmaCheck.Database.Enums;

namespace PharmaCheck.Web.Models.Supplier;

public sealed record NewSupplierHttpPostModel
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string AdditionAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
}
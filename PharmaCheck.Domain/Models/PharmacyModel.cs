using PharmaCheck.Database.Enums;

namespace PharmaCheck.Domain.Models;

public sealed class PharmacyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string AdditionAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public PharmacyType Type { get; set; }

    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}
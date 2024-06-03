using PharmaCheck.Database.Enums;

namespace PharmaCheck.Database.Entities;

public sealed record PharmacyEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string AdditionAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public PharmacyType Type { get; set; }

    public List<CheckEntity> Checks { get; set; } = new List<CheckEntity>();
    public List<PharmacyProductsEntity> Products { get; set; } = new List<PharmacyProductsEntity>();
    public List<SupplyEntity> Supplies { get; set; } = new List<SupplyEntity>();
}
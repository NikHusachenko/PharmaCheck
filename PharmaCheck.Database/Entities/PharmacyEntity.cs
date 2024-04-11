using PharmaCheck.Database.Enums;

namespace PharmaCheck.Database.Entities;

public sealed record PharmacyEntity : BaseEntity
{
    public string Name { get; set; }
    public string LocalCode { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string AdditionAddress { get; set; }
    public string ContactPhone { get; set; }
    public PharmacyType Type { get; set; }

    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
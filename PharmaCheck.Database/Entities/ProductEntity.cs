namespace PharmaCheck.Database.Entities;

public sealed record ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }
    public float Price { get; set; }
    public int Count { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }

    public Guid SupplyId { get; set; }
    public SupplyEntity Supply { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}
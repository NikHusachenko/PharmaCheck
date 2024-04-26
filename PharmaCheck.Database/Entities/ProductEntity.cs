namespace PharmaCheck.Database.Entities;

public sealed record ProductEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int Count { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

    public Guid TypeId { get; set; }
    public ProductTypeEntity ProductType { get; set; }

    public List<ProductSuppliesEntity> Supplies { get; set; } = new List<ProductSuppliesEntity>();
}
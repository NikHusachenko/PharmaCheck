namespace PharmaCheck.Database.Entities;

public sealed record ProductEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Count { get; set; }

    public Guid TypeId { get; set; }
    public ProductTypeEntity ProductType { get; set; }

    public List<PharmacyProductsEntity> Pharmacies { get; set; } = new List<PharmacyProductsEntity>();
    public List<ProductCheckEntity> Checks { get; set; } = new List<ProductCheckEntity>();
    public List<ProductSuppliesEntity> Supplies { get; set; } = new List<ProductSuppliesEntity>();
}
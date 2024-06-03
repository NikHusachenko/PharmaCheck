namespace PharmaCheck.Database.Entities;

public sealed record SupplyEntity : BaseEntity
{
    public DateTimeOffset? AppliedAt { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }

    public Guid SupplierId { get; set; }
    public SupplierEntity Supplier { get; set; }

    public List<ProductSuppliesEntity> Products { get; set; } = new List<ProductSuppliesEntity>();
}
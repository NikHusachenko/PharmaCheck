namespace PharmaCheck.Database.Entities;

public sealed record SupplyEntity : BaseEntity
{
    public List<ProductSuppliesEntity> Products { get; set; } = new List<ProductSuppliesEntity>(); 

    public Guid SupplierId { get; set; }
    public SupplierEntity Supplier { get; set; }
}
namespace PharmaCheck.Database.Entities;

public sealed record SupplyEntity : BaseEntity
{
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>(); 

    public Guid SupplierId { get; set; }
    public SupplierEntity Supplier { get; set; }
}
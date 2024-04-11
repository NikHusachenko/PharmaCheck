namespace PharmaCheck.Database.Entities;

public sealed record SupplyEntity : BaseEntity
{
    public Guid SupplierId { get; set; }
    public SupplierEntity Supplier { get; set; }
}
namespace PharmaCheck.Database.Entities;

public sealed class ProductSuppliesEntity
{
    public float Price { get; set; }

    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
    
    public Guid SupplyId { get; set; }
    public SupplyEntity Supply { get; set; }
}
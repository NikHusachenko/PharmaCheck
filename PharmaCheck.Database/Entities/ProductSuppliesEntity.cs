namespace PharmaCheck.Database.Entities;

public sealed record ProductSuppliesEntity
{
    public int Count { get; set; }

    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
    
    public Guid SupplyId { get; set; }
    public SupplyEntity Supply { get; set; }
}
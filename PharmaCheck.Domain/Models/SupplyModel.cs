namespace PharmaCheck.Domain.Models;

public sealed record SupplyModel
{
    public Guid Id { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}
namespace PharmaCheck.Domain.Models;

public sealed record CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<ProductTypeModel> ProductTypes { get; set; } = new List<ProductTypeModel>();
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}
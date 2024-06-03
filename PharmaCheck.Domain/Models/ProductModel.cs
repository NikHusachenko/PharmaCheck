namespace PharmaCheck.Domain.Models;

public sealed record ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Count { get; set; }

    public List<PharmacyModel> Pharmacies { get; set; }
    public SupplyModel Supply { get; set; }
    public ProductTypeModel ProductType { get; set; }
}
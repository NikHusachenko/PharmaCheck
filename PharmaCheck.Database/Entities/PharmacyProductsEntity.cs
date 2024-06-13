namespace PharmaCheck.Database.Entities;

public sealed record PharmacyProductsEntity : BaseEntity
{
    public int Count { get; set; }
    public int Reserved { get; set; }

    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }
}
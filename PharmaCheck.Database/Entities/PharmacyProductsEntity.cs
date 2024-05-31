namespace PharmaCheck.Database.Entities;

public sealed record PharmacyProductsEntity : BaseEntity
{
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }
}
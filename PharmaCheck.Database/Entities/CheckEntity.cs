namespace PharmaCheck.Database.Entities;

public sealed record CheckEntity : BaseEntity
{
    public bool IsPaid { get; set; }
    public DateTimeOffset? PaidAt { get; set; }

    public Guid? ClientId { get; set; }
    public ClientEntity Client { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }

    public List<ProductCheckEntity> Products { get; set; } = new List<ProductCheckEntity>();
}
namespace PharmaCheck.Database.Entities;

public sealed record CheckEntity : BaseEntity
{
    public Guid? ClientId { get; set; }
    public ClientEntity Client { get; set; }

    public List<ProductCheckEntity> Products { get; set; } = new List<ProductCheckEntity>();
}
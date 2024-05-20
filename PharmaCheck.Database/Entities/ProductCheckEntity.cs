namespace PharmaCheck.Database.Entities;

public sealed record ProductCheckEntity : BaseEntity
{
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }

    public Guid CheckId { get; set; }
    public CheckEntity Check { get; set; }
}
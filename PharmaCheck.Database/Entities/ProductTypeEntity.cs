namespace PharmaCheck.Database.Entities;

public sealed record ProductTypeEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
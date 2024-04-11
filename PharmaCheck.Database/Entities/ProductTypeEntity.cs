namespace PharmaCheck.Database.Entities;

public sealed record ProductTypeEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}
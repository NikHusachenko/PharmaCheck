namespace PharmaCheck.Database.Entities;

public sealed record CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    
    public List<ProductTypeEntity> Types {  get; set; } = new List<ProductTypeEntity>();
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
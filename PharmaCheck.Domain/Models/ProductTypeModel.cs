namespace PharmaCheck.Domain.Models;

public sealed record ProductTypeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryModel Category { get; set; }
}
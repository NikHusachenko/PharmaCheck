namespace PharmaCheck.Domain.Category.GetCategories;

public sealed record CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
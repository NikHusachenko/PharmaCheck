namespace PharmaCheck.Domain.Category.Models;

public sealed record CategoryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
using PharmaCheck.Domain.Category.Models;

namespace PharmaCheck.Domain.ProductType.Models;

public sealed record ProductTypeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryModel Category { get; set; }
}
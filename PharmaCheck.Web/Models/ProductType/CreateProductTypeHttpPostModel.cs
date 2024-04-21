namespace PharmaCheck.Web.Models.ProductType;

public sealed record CreateProductTypeHttpPostModel
{
    public string Name { get; set; }
    public required Guid CategoryId { get; set; }
}
namespace PharmaCheck.Web.Models.ProductType;

public sealed record GetProductTypesFilter
{
    public int Page { get; set; }
    public string NameQuery { get; set; } = string.Empty;
    public required Guid CategoryId { get; set; }
}
namespace PharmaCheck.Web.Models.Product;

public sealed record GetProductsFilter
{
    public Guid? PharmacyId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
}
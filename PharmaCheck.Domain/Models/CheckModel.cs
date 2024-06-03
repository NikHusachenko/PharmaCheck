namespace PharmaCheck.Domain.Models;

public sealed record CheckModel
{
    public float Price => Products.Sum(product => product.Price);
    public DateTimeOffset? PaidAt { get; set; }

    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}
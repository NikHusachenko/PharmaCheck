namespace PharmaCheck.Domain.Models;

public sealed record SupplierModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string AdditionAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;

    public List<SupplyModel> Supplies { get; set; } = new List<SupplyModel>();
}
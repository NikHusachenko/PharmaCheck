namespace PharmaCheck.Database.Entities;

public sealed record SupplierEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string AdditionAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;

    public List<SupplyEntity> Supplies { get; set; } = new List<SupplyEntity>();
}
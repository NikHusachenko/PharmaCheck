namespace PharmaCheck.Database.Entities;

public sealed record SupplierEntity : BaseEntity
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string AdditionAddress { get; set; }
    public string ContactPhone { get; set; }

    public List<SupplyEntity> Supplies { get; set; } = new List<SupplyEntity>();
}
using PharmaCheck.Database.Enums;

namespace PharmaCheck.Database.Entities;

public sealed record MedicineEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public float BuyPrice { get; set; }
    public float SellCoefficient { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Instruction { get; set; } = string.Empty;
    public MedicineType Type { get; set; }

    public Guid? OrderId { get; set; }
    public OrderEntity Order { get; set; }

    public Guid PharmacyId { get; set; }
    public PharmacyEntity Pharmacy { get; set; }
}
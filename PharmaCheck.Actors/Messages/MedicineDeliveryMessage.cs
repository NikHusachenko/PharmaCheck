using PharmaCheck.Database.Enums;

namespace PharmaCheck.Actors.Messages;

public sealed record MedicineDeliveryMessage
{
    public string Name { get; set; } = string.Empty;
    public float BuyPrice { get; set; }
    public float SellCoefficient { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Instruction { get; set; } = string.Empty;
    public MedicineType Type { get; set; }
}
using PharmaCheck.Database.Enums;

namespace PharmaCheck.Database.Entities;

public sealed record PharmacyEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public PharmacyType Type { get; set; }
    
    public List<MedicineEntity> Medicines { get; set; }

    public PharmacyEntity()
    {
        Medicines = new List<MedicineEntity>();
    }
}
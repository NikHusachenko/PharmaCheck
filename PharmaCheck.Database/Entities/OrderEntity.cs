namespace PharmaCheck.Database.Entities;

public sealed record OrderEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public List<MedicineEntity> Medicines { get; set; }

    public OrderEntity()
    {
        Medicines = new List<MedicineEntity>();
    }
}
namespace PharmaCheck.Database.Entities;

public sealed record UserEntity : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public List<OrderEntity> Orders { get; set; }

    public UserEntity()
    {
        Orders = new List<OrderEntity>();
    }
}
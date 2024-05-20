namespace PharmaCheck.Database.Entities;

public sealed record ClientEntity : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public List<CheckEntity> Checks { get; set; } = new List<CheckEntity>();
}
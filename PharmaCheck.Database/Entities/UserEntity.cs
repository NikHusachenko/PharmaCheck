namespace PharmaCheck.Database.Entities;

public sealed record UserEntity : BaseEntity
{
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public byte[] SaltByte { get; set; } = new byte[0];
    public string Phone { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
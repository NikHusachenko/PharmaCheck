namespace PharmaCheck.Services.JwtServices;

public sealed record JwtOptions
{
    public string SecurityKey { get; set; } = string.Empty;
    public int ExpirationTime { get; set; }
}
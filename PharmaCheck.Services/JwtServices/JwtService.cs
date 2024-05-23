using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PharmaCheck.Services.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmaCheck.Services.JwtServices;

public sealed class JwtService : IJwtService
{
    private const string CantWriteTokenError = "Can't write token.";
    private const string CantReadTokenError = "Can't read token.";

    private readonly JwtOptions _options;

    public JwtService(IOptionsMonitor<JwtOptions> monitor)
    {
        _options = monitor.CurrentValue;
    }

    public Result<IEnumerable<Claim>> Decode(string token)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        return handler.CanReadToken(token) ?
            Result<IEnumerable<Claim>>.Ok(handler.ReadJwtToken(token).Claims, ResultSuccessStatusCode.Ok) :
            Result<IEnumerable<Claim>>.Error(CantReadTokenError, ResultErrorStatusCode.Unauthorized);
    }

    public Result<string> Encode(IEnumerable<Claim> claims)
    {
        byte[] byteKey = Encoding.UTF8.GetBytes(_options.SecurityKey);
        SecurityKey securityKey = new SymmetricSecurityKey(byteKey);
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.Now.AddSeconds(_options.ExpirationTime));

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        try
        {
            return Result<string>.Ok(handler.WriteToken(jwtSecurityToken), ResultSuccessStatusCode.Ok);
        }
        catch
        {
            return Result<string>.Error(CantWriteTokenError, ResultErrorStatusCode.InternalError);
        }
    }
}
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PharmaCheck.Services.Response;
using System.Security.Claims;

namespace PharmaCheck.Services.JwtServices;

public interface IJwtService
{
    Result<string> Encode(IEnumerable<Claim> claim);
    Result<IEnumerable<Claim>> Decode(string token);
}
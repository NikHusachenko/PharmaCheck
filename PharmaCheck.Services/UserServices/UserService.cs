using PharmaCheck.Database.Entities;
using System.Security.Claims;

namespace PharmaCheck.Services.UserServices;

public sealed class UserService : IUserService
{
    public IEnumerable<Claim> GetClaims(UserEntity user) =>
    [
        new Claim(JwtClaimTypes.Id, user.Id.ToString()),
        new Claim(JwtClaimTypes.Login, user.Login),
        new Claim(JwtClaimTypes.FirstName, user.FirstName),
        new Claim(JwtClaimTypes.LastName, user.LastName)
    ];
}
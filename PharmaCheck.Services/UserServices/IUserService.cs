using PharmaCheck.Database.Entities;
using System.Security.Claims;

namespace PharmaCheck.Services.UserServices;

public interface IUserService
{
    IEnumerable<Claim> GetClaims(UserEntity user);
}
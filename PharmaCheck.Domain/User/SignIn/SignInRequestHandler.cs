using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.HashingServices;
using PharmaCheck.Services.JwtServices;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.UserServices;
using System.Text;

namespace PharmaCheck.Domain.User.SignIn;

public sealed class SignInRequestHandler(
    IRepositoryFactory repositoryFactory,
    IJwtService jwtService,
    IUserService userService)
    : IRequestHandler<SignInRequest, Result<string>>
{
    private const string InvalidCredentialsError = "Invalid credentials.";

    public async Task<Result<string>> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        UserRepository repository = repositoryFactory.NewUserRepository();

        UserEntity? dbRecord = await repository.GetByLogin(request.Login);
        if (dbRecord is null)
        {
            return Result<string>.Error(InvalidCredentialsError, ResultErrorStatusCode.NotFound);
        }

        if (!Hasher.Verify(request.Password, dbRecord.HashedPassword, dbRecord.SaltByte))
        {
            return Result<string>.Error(InvalidCredentialsError, ResultErrorStatusCode.NotFound);
        }

        Result<string> tokenResult = jwtService.Encode(userService.GetClaims(dbRecord));
        return tokenResult.IsError ?
            Result<string>.Error(tokenResult.ErrorMessage, ResultErrorStatusCode.Unauthorized) :
            Result<string>.Ok(tokenResult.Value, ResultSuccessStatusCode.Ok);
    }
}
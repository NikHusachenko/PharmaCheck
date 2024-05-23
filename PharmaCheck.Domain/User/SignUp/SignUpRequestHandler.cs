using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.HashingServices;
using PharmaCheck.Services.JwtServices;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.UserServices;

namespace PharmaCheck.Domain.User.SignUp;

public sealed class SignUpRequestHandler(
    IRepositoryFactory repositoryFactory,
    IJwtService jwtService,
    IUserService userService)
    : IRequestHandler<SignUpRequest, Result<string>>
{
    private const string WasCreatedError = "User was created";
    private const string RegistrationError = "Error while registration new user";

    public async Task<Result<string>> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        (string hash, byte[] salt) = Hasher.Hash(request.Password);

        UserRepository repository = repositoryFactory.NewUserRepository();

        UserEntity? userEntity = await repository.GetByLogin(request.Login);
        if (userEntity is not null)
        {
            return Result<string>.Error(WasCreatedError, ResultErrorStatusCode.BadRequest);
        }

        userEntity = new UserEntity()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Login = request.Login,
            Phone = request.Phone,
            HashedPassword = hash,
            SaltByte = salt
        };

        try
        {
            await repository.Create(userEntity);
        }
        catch
        {
            return Result<string>.Error(RegistrationError, ResultErrorStatusCode.InternalError);
        }

        Result<string> tokenResult = jwtService.Encode(userService.GetClaims(userEntity));
        return tokenResult.IsError ?
            Result<string>.Error(tokenResult.ErrorMessage, (ResultErrorStatusCode)tokenResult.StatusCode) :
            Result<string>.Ok(tokenResult.Value, ResultSuccessStatusCode.Ok);
    }
}
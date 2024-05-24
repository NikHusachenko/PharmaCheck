using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.User.SignIn;

public sealed record SignInRequest(string Login, string Password) : IRequest<Result<string>>;
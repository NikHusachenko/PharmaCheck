using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.User.SignUp;

public sealed record SignUpRequest(string Login, string Password, string Phone, string FirstName, string LastName) : IRequest<Result<string>>;
using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.Pay;

public sealed record PayCheckRequest(Guid Id) : IRequest<Result>;
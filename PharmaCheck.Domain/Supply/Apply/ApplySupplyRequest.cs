using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.Apply;

public sealed record ApplySupplyRequest(Guid Id) : IRequest<Result>;
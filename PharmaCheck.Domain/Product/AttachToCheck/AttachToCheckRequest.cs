using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.AttachToCheck;

public sealed record AttachToCheckRequest(Guid CheckId, Guid ProductId) : IRequest<Result>;
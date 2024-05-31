using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.RemoveFromCheck;

public sealed record RemoveFromCheckRequest(Guid ProductId, Guid CheckId) : IRequest<Result>;
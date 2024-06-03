using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.RemoveFromOrder;

public sealed record RemoveProductFromOrderRequest(Guid ProductId, Guid OrderId) : IRequest<Result>;
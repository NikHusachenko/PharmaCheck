using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.AppendToOrder;

public sealed record AppendProductToOrderRequest(Guid ProductId, Guid SupplyId, int Count) : IRequest<Result>;
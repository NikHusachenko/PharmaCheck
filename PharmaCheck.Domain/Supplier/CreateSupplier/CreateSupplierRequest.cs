using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supplier.CreateSupplier;

public sealed record CreateSupplierRequest(
    string Name,
    string Region,
    string City,
    string Street,
    string AdditionAddress,
    string ContactPhone)
    : IRequest<Result<Guid>>;
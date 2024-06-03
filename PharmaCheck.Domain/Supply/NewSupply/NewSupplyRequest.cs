using MediatR;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.NewSupply;

public sealed record NewSupplyRequest(Guid SupplierId, Guid PharmacyId) : IRequest<Result<Guid>>;
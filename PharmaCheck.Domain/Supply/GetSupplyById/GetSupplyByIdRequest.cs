using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.GetSupplyById;

public sealed record GetSupplyByIdRequest(Guid Id) : IRequest<Result<SupplyModel>>;
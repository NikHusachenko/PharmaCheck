using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.New;

public sealed record NewCheckRequest(Guid PharmacyId, Guid? ClientId) : IRequest<Result<CheckEntity>>;
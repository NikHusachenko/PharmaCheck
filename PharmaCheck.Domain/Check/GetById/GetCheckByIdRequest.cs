using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.GetById;

public sealed record GetCheckByIdRequest(Guid Id) : IRequest<Result<CheckModel>>;
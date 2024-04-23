using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacyById;

public sealed record GetPharmacyByIdRequest(Guid Id) : IRequest<Result<PharmacyModel>>;
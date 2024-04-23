using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacyByName;

public sealed record GetPharmacyByNameRequest(string Name) : IRequest<Result<PharmacyModel>>;
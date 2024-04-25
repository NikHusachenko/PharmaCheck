using MediatR;
using PharmaCheck.Domain.Models;

namespace PharmaCheck.Domain.Pharmacy.GetPharmaciesByProductId;

public sealed record GetPharmaciesByProductIdRequest(Guid Id) : IRequest<List<PharmacyModel>>;
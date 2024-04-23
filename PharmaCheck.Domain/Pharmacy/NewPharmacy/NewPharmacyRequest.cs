using MediatR;
using PharmaCheck.Database.Enums;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Pharmacy.NewPharmacy;

public sealed record NewPharmacyRequest(
    string Name,
    string Region,
    string City,
    string Street,
    string AdditionAddress,
    string ContactPhone,
    PharmacyType Type) : IRequest<Result<Guid>>;
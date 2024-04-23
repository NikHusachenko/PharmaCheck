using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Pharmacy.NewPharmacy;

public sealed class NewPharmacyRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<NewPharmacyRequestHandler> logger)
    : IRequestHandler<NewPharmacyRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(NewPharmacyRequest request, CancellationToken cancellationToken)
    {
        PharmacyRepository repository = repositoryFactory.NewPharmacyRepository();
        
        if ((await repository.CheckByAddress(request.City,
            request.Region,
            request.Street,
            request.AdditionAddress)) is true)
        {
            return Result<Guid>.Error("Pharmacy in this address was exists.", ResultErrorStatusCode.BadRequest);
        }

        PharmacyEntity entity = new PharmacyEntity()
        {
            AdditionAddress = request.AdditionAddress,
            City = request.City,
            ContactPhone = request.ContactPhone,
            Name = request.Name,
            Region = request.Region,
            Street = request.Street,
            Type = request.Type
        };

        try
        {
            await repository.Create(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Can't create pharmacy [{entity.Name}] in address [{entity.Region}, {entity.City}, {entity.Street}, {entity.AdditionAddress}]: {ex.Message}");
            return Result<Guid>.Error("Can't create pharmacy.", ResultErrorStatusCode.InternalError);
        }
        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Ok);
    }
}
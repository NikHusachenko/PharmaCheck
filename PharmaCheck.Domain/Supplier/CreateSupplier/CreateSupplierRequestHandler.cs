using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supplier.CreateSupplier;

public sealed class CreateSupplierRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<CreateSupplierRequestHandler> logger)
    : IRequestHandler<CreateSupplierRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        SupplierRepository repository = repositoryFactory.NewSupplierRepository();
        if ((await repository.CheckByAddress(request.Region, request.City, request.Street, request.AdditionAddress)) is true)
        {
            return Result<Guid>.Error("Was created", ResultErrorStatusCode.BadRequest);
        }

        SupplierEntity entity = new SupplierEntity()
        {
            AdditionAddress = request.AdditionAddress,
            City = request.City,
            ContactPhone = request.ContactPhone,
            Name = request.Name,
            Street = request.Street,
            Region = request.Region,
        };


        try
        {
            await repository.Create(entity);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Error("Can't create supplier", ResultErrorStatusCode.InternalError);
        }
        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Created);
    }
}
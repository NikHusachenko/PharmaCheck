using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Supplier.GetSupplierById;

public sealed class GetSupplierByIdRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSupplierByIdRequest, Result<SupplierModel>>
{
    public async Task<Result<SupplierModel>> Handle(GetSupplierByIdRequest request, CancellationToken cancellationToken)
    {
        SupplierRepository repository = factory.NewSupplierRepository();
        return await repository.GetById(request.Id)
            .Map(entity => entity is null ?
                Result<SupplierModel>.Error("Supplier not found.", ResultErrorStatusCode.NotFound) :
                Result<SupplierModel>.Ok(new SupplierModel()
                {
                    AdditionAddress = entity.AdditionAddress,
                    City = entity.City,
                    ContactPhone = entity.ContactPhone,
                    Id = entity.Id,
                    Name = entity.Name,
                    Region = entity.Region,
                    Street = entity.Street,
                    Supplies = entity.Supplies.Select(supply => new SupplyModel()
                    {
                        Id = supply.Id,
                    }).ToList(),
                },
                ResultSuccessStatusCode.Ok));
    }
}
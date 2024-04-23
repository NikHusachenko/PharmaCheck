using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;

namespace PharmaCheck.Domain.Supplier.GetSuppliers;

public sealed class GetSuppliersRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSuppliersRequest, List<SupplierModel>>
{
    private const int PAGE_VOLUME = 30;

    public async Task<List<SupplierModel>> Handle(GetSuppliersRequest request, CancellationToken cancellationToken)
    {
        SupplierRepository repository = factory.NewSupplierRepository();

        int skip = request.Page <= 0 ? 0 : PAGE_VOLUME * (request.Page - 1);
        return (await repository.GetAll(skip,
            PAGE_VOLUME,
            request.NameQuery,
            request.RegionQuery,
            request.CityQuery,
            request.StreetQuery,
            request.AdditionAddressQuery,
            request.ContactPhoneQuery))
                .Select(entity => new SupplierModel()
                {
                    AdditionAddress = entity.AdditionAddress,
                    ContactPhone = entity.ContactPhone,
                    Street = entity.Street,
                    City = entity.City,
                    Id = entity.Id,
                    Name = entity.Name,
                    Region = entity.Region,
                    Supplies = entity.Supplies.Select(supply => new SupplyModel()
                    {
                        Id = supply.Id,
                    })
                    .ToList()
                })
                .ToList();
    }
}
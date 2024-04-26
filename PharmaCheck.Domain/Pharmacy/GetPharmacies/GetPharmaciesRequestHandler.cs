using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacies;

public sealed class GetPharmaciesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetPharmaciesRequest, List<PharmacyModel>>
{
    public async Task<List<PharmacyModel>> Handle(GetPharmaciesRequest request, CancellationToken cancellationToken)
    {
        PharmacyRepository repository = factory.NewPharmacyRepository();
        return await repository.GetAll(request.CityQuery, request.RegionQuery, request.StreetQuery, request.AdditionAddressQuery)
            .Map<List<PharmacyEntity>, List<PharmacyModel>>(list => list.Select(item => new PharmacyModel()
            {
                AdditionAddress = item.AdditionAddress,
                City = item.City,
                ContactPhone = item.ContactPhone,
                Name = item.Name,
                Region = item.Region,
                Street = item.Street,
                Type = item.Type,
                Products = item.Products.Select(product => new ProductModel()
                {
                    Category = new CategoryModel()
                    {
                        Name = product.Category.Name,
                        Id = product.Category.Id,
                    },
                    Count = product.Count,
                    Description = product.Description,
                    Id = product.Id,
                    Manufacturer = product.Manufacturer,
                    Name = product.Name,
                    ProductType = new ProductTypeModel()
                    {
                        Id = product.ProductType.Id,
                        Name = product.ProductType.Name,
                    }
                })
                .ToList()
            })
            .ToList());
    }
}
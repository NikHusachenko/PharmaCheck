using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacies;

public sealed class GetPharmaciesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetPharmaciesRequest, List<PharmacyModel>>
{
    public async Task<List<PharmacyModel>> Handle(GetPharmaciesRequest request, CancellationToken cancellationToken)
    {
        PharmacyRepository repository = factory.NewPharmacyRepository();
        return await repository.GetAll(request.CityQuery, request.RegionQuery, request.StreetQuery, request.AdditionAddressQuery)
            .Map(list => list.Select(item => new PharmacyModel()
            {
                Id = item.Id,
                AdditionAddress = item.AdditionAddress,
                City = item.City,
                ContactPhone = item.ContactPhone,
                Name = item.Name,
                Region = item.Region,
                Street = item.Street,
                Type = item.Type,
                Products = item.Products.Select(product => new ProductModel()
                {
                    Count = product.Product.Count,
                    Description = product.Product.Description,
                    Id = product.Id,
                    Manufacturer = product.Product.Manufacturer,
                    Name = product.Product.Name,
                    ProductType = new ProductTypeModel()
                    {
                        Id = product.Product.ProductType.Id,
                        Name = product.Product.ProductType.Name,
                        Category = new CategoryModel()
                        {
                            Name = product.Product.ProductType.Category.Name,
                            Id = product.Product.ProductType.Category.Id,
                        }
                    }
                })
                .ToList()
            })
            .ToList());
    }
}
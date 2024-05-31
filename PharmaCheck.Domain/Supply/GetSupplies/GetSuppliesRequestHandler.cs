using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Supply.GetSupplies;

public sealed class GetSuppliesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSuppliesRequest, List<SupplyModel>>
{
    public async Task<List<SupplyModel>> Handle(GetSuppliesRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository repository = factory.NewSupplyRepository();
        return await repository.GetAll(request.SupplierId)
            .Map<List<SupplyEntity>, List<SupplyModel>>(list => list.Any() ?
                list.Select(supply => new SupplyModel()
                {
                    Id = supply.Id,
                    Products = supply.Products.Select(product => new ProductModel()
                    {
                        Count = product.Product.Count,
                        Description = product.Product.Description,
                        Id = product.Product.Id,
                        Manufacturer = product.Product.Manufacturer,
                        Name = product.Product.Name,
                        ProductType = new ProductTypeModel()
                        {
                            Id = product.Product.ProductType.Id,
                            Name = product.Product.ProductType.Name,
                            Category = new CategoryModel()
                            {
                                Id = product.Product.ProductType.Category.Id,
                                Name = product.Product.ProductType.Category.Name,
                            }
                        },
                        Pharmacies = product.Product.Pharmacies.Select(x => new PharmacyModel()
                        {
                            AdditionAddress = x.Pharmacy.AdditionAddress,
                            City = x.Pharmacy.City,
                            ContactPhone = x.Pharmacy.ContactPhone,
                            Name = x.Pharmacy.Name,
                            Region = x.Pharmacy.Region,
                            Street = x.Pharmacy.Street,
                            Type = x.Pharmacy.Type,
                        }).ToList(),
                    })
                    .ToList()
                })
                .ToList():
                new List<SupplyModel>());
    }
}
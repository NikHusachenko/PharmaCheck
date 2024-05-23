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
                        Category = new CategoryModel()
                        {
                            Id = product.Product.Category.Id,
                            Name = product.Product.Category.Name
                        },
                        Count = product.Product.Count,
                        Description = product.Product.Description,
                        Id = product.Product.Id,
                        Manufacturer = product.Product.Manufacturer,
                        Name = product.Product.Name,
                        ProductType = new ProductTypeModel()
                        {
                            Id = product.Product.ProductType.Id,
                            Name = product.Product.ProductType.Name,
                        },
                        Pharmacy = new PharmacyModel()
                        {
                            AdditionAddress = product.Product.Pharmacy.AdditionAddress,
                            City = product.Product.Pharmacy.City,
                            ContactPhone = product.Product.Pharmacy.ContactPhone,
                            Name = product.Product.Pharmacy.Name,
                            Region = product.Product.Pharmacy.Region,
                            Street = product.Product.Pharmacy.Street,
                            Type = product.Product.Pharmacy.Type,
                        }
                    })
                    .ToList()
                })
                .ToList():
                new List<SupplyModel>());
    }
}
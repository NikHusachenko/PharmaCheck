using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Supply.GetSupplies;

public sealed class GetSuppliesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSuppliesRequest, List<SupplyModel>>
{
    public async Task<List<SupplyModel>> Handle(GetSuppliesRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository repository = factory.NewSupplyRepository();
        return await repository.GetAll(request.SupplierId)
            .Map(list => list.Any() ?
                list.Select(supply => new SupplyModel()
                {
                    Id = supply.Id,
                    Products = supply.Products.Select(product => new ProductModel()
                    {
                        Category = new CategoryModel()
                        {
                            Id = product.Category.Id,
                            Name = product.Category.Name
                        },
                        Count = product.Count,
                        Description = product.Description,
                        Id = product.Id,
                        Manufacturer = product.Manufacturer,
                        Name = product.Name,
                        Price = product.Price,
                        ProductType = new ProductTypeModel()
                        {
                            Id = product.ProductType.Id,
                            Name = product.ProductType.Name,
                        },
                        Pharmacy = new PharmacyModel()
                        {
                            AdditionAddress = product.Pharmacy.AdditionAddress,
                            City = product.Pharmacy.City,
                            ContactPhone = product.Pharmacy.ContactPhone,
                            Name = product.Pharmacy.Name,
                            Region = product.Pharmacy.Region,
                            Street = product.Pharmacy.Street,
                            Type = product.Pharmacy.Type,
                        }
                    })
                    .ToList()
                })
                .ToList():
                new List<SupplyModel>());
    }
}
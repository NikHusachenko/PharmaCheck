using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Supplier.GetSupplierByName;

public sealed class GetSupplierByNameRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSupplierByNameRequest, Result<SupplierModel>>
{
    public async Task<Result<SupplierModel>> Handle(GetSupplierByNameRequest request, CancellationToken cancellationToken)
    {
        SupplierRepository repository = factory.NewSupplierRepository();
        return await repository.GetByName(request.Name)
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
                        Products = supply.Products.Select(product => new ProductModel()
                        {
                            Count = product.Product.Count,
                            Description = product.Product.Description,
                            Id = product.Product.Id,
                            Manufacturer = product.Product.Manufacturer,
                            Name = product.Product.Name,
                            Category = new CategoryModel()
                            {
                                Id = product.Product.Category.Id,
                                Name = product.Product.Category.Name,
                                ProductTypes = product.Product.Category.Types.Select(type => new ProductTypeModel()
                                {
                                    Id = type.Id,
                                    Name = type.Name,
                                })
                                .ToList()
                            }
                        })
                        .ToList()
                    })
                    .ToList()
                },
                ResultSuccessStatusCode.Ok));
    }
}
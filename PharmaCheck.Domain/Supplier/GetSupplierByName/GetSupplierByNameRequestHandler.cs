using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;

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
                            Count = product.Count,
                            Description = product.Description,
                            Id = product.Id,
                            Manufacturer = product.Manufacturer,
                            Name = product.Name,
                            Price = product.Price,
                            Category = new CategoryModel()
                            {
                                Id = product.Category.Id,
                                Name = product.Category.Name,
                                ProductTypes = product.Category.Types.Select(type => new ProductTypeModel()
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
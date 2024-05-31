using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Pharmacy.GetPharmacyById;

public sealed class GetPharmacyByIdRequestHandler(IRepositoryFactory factory) : IRequestHandler<GetPharmacyByIdRequest, Result<PharmacyModel>>
{
    public async Task<Result<PharmacyModel>> Handle(GetPharmacyByIdRequest request, CancellationToken cancellationToken)
    {
        PharmacyRepository repository = factory.NewPharmacyRepository();
        return await repository.GetById(request.Id)
            .Map(entity => entity is null ?
                Result<PharmacyModel>.Error("Pharmacy not found.", ResultErrorStatusCode.NotFound) :
                Result<PharmacyModel>.Ok(new PharmacyModel()
                {
                    AdditionAddress = entity.AdditionAddress,
                    City = entity.City,
                    ContactPhone = entity.ContactPhone,
                    Name = entity.Name,
                    Region = entity.Region,
                    Street = entity.Street,
                    Products = entity.Products.Select(product => new ProductModel()
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
                                Id = product.Product.ProductType.Category.Id,
                                Name = product.Product.ProductType.Category.Name,
                            }
                        }
                    })
                    .ToList()
                },
                ResultSuccessStatusCode.Ok));
    }
}
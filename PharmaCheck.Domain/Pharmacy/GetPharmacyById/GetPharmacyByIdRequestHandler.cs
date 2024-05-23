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
                        Count = product.Count,
                        Description = product.Description,
                        Id = product.Id,
                        Manufacturer = product.Manufacturer,
                        Name = product.Name,
                        Category = new CategoryModel()
                        {
                            Id = product.Category.Id,
                            Name = product.Category.Name,
                        },
                        ProductType = new ProductTypeModel()
                        {
                            Id= product.ProductType.Id,
                            Name = product.ProductType.Name,
                        }
                    })
                    .ToList()
                },
                ResultSuccessStatusCode.Ok));
    }
}
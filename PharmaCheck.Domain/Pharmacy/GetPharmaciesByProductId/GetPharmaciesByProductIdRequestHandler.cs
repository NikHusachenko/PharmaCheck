using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Pharmacy.GetPharmaciesByProductId;

public sealed class GetPharmaciesByProductIdRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetPharmaciesByProductIdRequest, List<PharmacyModel>>
{
    public async Task<List<PharmacyModel>> Handle(GetPharmaciesByProductIdRequest request, CancellationToken cancellationToken)
    {
        PharmacyRepository repository = factory.NewPharmacyRepository();
        return await repository.GetByProductId(request.Id)
            .Map<List<PharmacyEntity>, List<PharmacyModel>>(list => list.Select(item => new PharmacyModel()
            {
                AdditionAddress = item.AdditionAddress,
                City = item.City,
                ContactPhone = item.ContactPhone,
                Name = item.Name,
                Products = item.Products.Select(product => new ProductModel()
                {
                    Description = product.Product.Description,
                    Count = product.Product.Count,
                    Id = product.Product.Id,
                    Manufacturer = product.Product.Manufacturer,
                    Name = product.Product.Name
                })
                .ToList(),
                Region = item.Region,
                Street = item.Street,
                Type = item.Type,
            })
            .ToList());
    }
}
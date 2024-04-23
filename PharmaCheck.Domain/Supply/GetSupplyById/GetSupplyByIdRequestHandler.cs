using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Supply.GetSupplyById;

public sealed class GetSupplyByIdRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetSupplyByIdRequest, Result<SupplyModel>>
{
    public async Task<Result<SupplyModel>> Handle(GetSupplyByIdRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository repository = factory.NewSupplyRepository();
        return await repository.GetById(request.SupplierId, request.Id)
            .Map(entity => entity is null ?
                Result<SupplyModel>.Error("Supply not found.", ResultErrorStatusCode.NotFound) :
                Result<SupplyModel>.Ok(new SupplyModel()
                {
                    Id = request.Id,
                    Products = entity.Products.Select(product => new ProductModel()
                    {
                        Count = product.Count,
                        Description = product.Description,
                        Id = product.Id,
                        Manufacturer = product.Manufacturer,
                        Name = product.Name,
                        Price = product.Price,
                        Category = new CategoryModel()
                        {
                            Name = product.Category.Name,
                            Id = product.Category.Id,
                        },
                        ProductType = new ProductTypeModel()
                        {
                            Id = product.ProductType.Id,
                            Name = product.ProductType.Name
                        }
                    })
                    .ToList()
                },
                ResultSuccessStatusCode.Ok));
    }
}
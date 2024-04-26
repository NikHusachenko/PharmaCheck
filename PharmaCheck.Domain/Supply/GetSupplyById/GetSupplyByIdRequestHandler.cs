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
                        Count = product.Product.Count,
                        Description = product.Product.Description,
                        Id = product.Product.Id,
                        Manufacturer = product.Product.Manufacturer,
                        Name = product.Product.Name,
                        Category = new CategoryModel()
                        {
                            Name = product.Product.Category.Name,
                            Id = product.Product.Category.Id,
                        },
                        ProductType = new ProductTypeModel()
                        {
                            Id = product.Product.ProductType.Id,
                            Name = product.Product.ProductType.Name
                        }
                    })
                    .ToList()
                },
                ResultSuccessStatusCode.Ok));
    }
}
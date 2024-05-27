using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.ProductType.GetProductTypeByName;

public sealed class GetProductTypeByNameRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetProductTypeByNameRequest, Result<ProductTypeModel>>
{
    public async Task<Result<ProductTypeModel>> Handle(GetProductTypeByNameRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = factory.NewProductTypeRepository();
        return await repository.GetByName(request.Name)
            .Map(entity => entity is null ?
                Result<ProductTypeModel>.Error("Product type not found.", ResultErrorStatusCode.NotFound) :
                Result<ProductTypeModel>.Ok(new ProductTypeModel()
                {
                    Category = new CategoryModel()
                    {
                        Id = entity.Category.Id,
                        Name = entity.Category.Name
                    },
                    Id = entity.Id,
                    Name = entity.Name
                },
                ResultSuccessStatusCode.Ok));
    }
}
using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.ProductType.GetProductTypeById;

public sealed class GetProductTypeByIdRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetProductTypeByIdRequest, Result<ProductTypeModel>>
{
    public async Task<Result<ProductTypeModel>> Handle(GetProductTypeByIdRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = factory.NewProductTypeRepository();
        return await repository.GetById(request.CategoryId, request.Id)
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
using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.ProductType.GetProductTypes;

public sealed class GetProductTypesRequestHandler(IRepositoryFactory repositoryFactory)
    : IRequestHandler<GetProductTypesRequest, IEnumerable<ProductTypeModel>>
{
    private const int PAGE_VOLUME = 20;

    public async Task<IEnumerable<ProductTypeModel>> Handle(GetProductTypesRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = repositoryFactory.NewProductTypeRepository();

        int skip = request.Page.HasValue ? (request.Page.Value <= 0 ? 0 : PAGE_VOLUME * (request.Page.Value - 1)) : 0;
        return await repository.GetAll(skip, PAGE_VOLUME, request.QueryName, request.CategoryId).Map(type => new ProductTypeModel()
        {
            Category = new CategoryModel()
            {
                Name = type.Category.Name,
                Id = type.Category.Id,
            },
            Id = type.Id,
            Name = type.Name
        });
    }
}
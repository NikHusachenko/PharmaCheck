using MediatR;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed class GetCategoriesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetCategoriesRequest, IEnumerable<CategoryModel>>
{
    public async Task<IEnumerable<CategoryModel>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = factory.NewCategoryRepository();
        return await repository.GetAll(request.Query).Map(category => new CategoryModel()
        {
            Id = category.Id,
            Name = category.Name,
        });
    }
}
using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Category.GetCategories;

public sealed class GetCategoriesRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetCategoriesRequest, IEnumerable<CategoryModel>>
{
    private const int PAGE_VOLUME = 20;

    public async Task<IEnumerable<CategoryModel>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = factory.NewCategoryRepository();

        int skip = request.Page <= 0 ? 0 : PAGE_VOLUME * (request.Page - 1);
        return await repository.GetAll(skip, PAGE_VOLUME, request.Query).Map(category => new CategoryModel()
        {
            Id = category.Id,
            Name = category.Name,
        });
    }
}
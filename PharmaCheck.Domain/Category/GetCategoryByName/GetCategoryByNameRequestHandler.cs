using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Utilities.Extensions;

namespace PharmaCheck.Domain.Category.GetCategoryByName;

public sealed class GetCategoryByNameRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetCategoryByNameRequest, Result<CategoryModel>>
{
    private const string CategoryNotFoundError = "Category not found.";

    public async Task<Result<CategoryModel>> Handle(GetCategoryByNameRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = factory.NewCategoryRepository();
        return await repository.GetByName(request.Name).Map(category => category is null ?
            Result<CategoryModel>.Error(CategoryNotFoundError, ResultErrorStatusCode.NotFound) :
            Result<CategoryModel>.Ok(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
            },
            ResultSuccessStatusCode.Ok));
    }
}
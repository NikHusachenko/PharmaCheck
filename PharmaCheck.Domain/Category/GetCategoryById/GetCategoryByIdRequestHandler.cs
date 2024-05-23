using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;
using PharmaCheck.Services.Extensions;

namespace PharmaCheck.Domain.Category.GetCategoryById;

public sealed class GetCategoryByIdRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<GetCategoryByIdRequest, Result<CategoryModel>>
{
    public async Task<Result<CategoryModel>> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = factory.NewCategoryRepository();
        return await repository.GetById(request.Id).Map(category =>
            category is null ?
            Result<CategoryModel>.Error("", ResultErrorStatusCode.NotFound) :
            Result<CategoryModel>.Ok(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            },
            ResultSuccessStatusCode.Ok));
    }
}
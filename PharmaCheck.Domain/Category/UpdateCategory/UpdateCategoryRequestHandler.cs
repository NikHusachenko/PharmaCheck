using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.UpdateCategory;

public sealed class UpdateCategoryRequestHandler(
    IRepositoryFactory factory,
    ILogger<UpdateCategoryRequestHandler> logger)
    : IRequestHandler<UpdateCategoryRequest, Result>
{
    public async Task<Result> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = factory.NewCategoryRepository();

        CategoryEntity? entity = await repository.GetById(request.Id);
        if (entity is null)
        {
            return Result.Error("Category not found", ResultErrorStatusCode.NotFound);
        }

        entity.Name = request.NewName;

        try
        {
            await repository.Update(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Update error: {ex.Message}");
            return Result.Error($"Update error.", ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.Ok);
    }
}
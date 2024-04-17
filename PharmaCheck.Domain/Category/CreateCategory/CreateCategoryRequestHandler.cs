using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.CreateCategory;

public sealed class CreateCategoryRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<CreateCategoryRequestHandler> logger)
    : IRequestHandler<CreateCategoryRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        CategoryRepository repository = repositoryFactory.NewCategoryRepository();
        CategoryEntity entity = new() { Name = request.Name };
        try
        {
            await repository.Create(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Can't create category [{request.Name}]");
            return Result<Guid>.Error("Can't create new category", ResultErrorStatusCode.BadRequest);
        }
        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Ok);
    }
}
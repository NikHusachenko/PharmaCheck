using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.AttachTypeToCategory;

public sealed class AttachTypeToCategoryRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<AttachTypeToCategoryRequestHandler> logger)
    : IRequestHandler<AttachTypeToCategoryRequest, Result>
{
    public async Task<Result> Handle(AttachTypeToCategoryRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = repositoryFactory.NewProductTypeRepository();

        ProductTypeEntity? entity = await repository.GetById(request.CategoryId, request.TypeId);
        if (entity is null)
        {
            return Result.Error("Product type not found.", ResultErrorStatusCode.NotFound);
        }

        entity.CategoryId = request.CategoryId;

        try
        {
            await repository.Update(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Can't attach product type [{entity.Id}] to category [{request.CategoryId}]: {ex.Message}");
            return Result.Error("Can't change category for this product type.", ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
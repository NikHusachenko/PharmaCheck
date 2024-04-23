using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.UpdateProductTypeName;

public sealed class UpdateProductTypeNameRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<UpdateProductTypeNameRequestHandler> logger)
    : IRequestHandler<UpdateProductTypeNameRequest, Result>
{
    public async Task<Result> Handle(UpdateProductTypeNameRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = repositoryFactory.NewProductTypeRepository();
        ProductTypeEntity? entity = await repository.GetById(request.CategoryId, request.Id);

        if (entity is null)
        {
            return Result.Error("Product type not found.", ResultErrorStatusCode.NotFound);
        }

        entity.Name = request.NewName;

        try
        {
            await repository.Update(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Can't change product type [{entity.Name}] to [{request.NewName}]: {ex.Message}");
            return Result.Error("Can't update product type name.", ResultErrorStatusCode.InternalError);
        }

        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
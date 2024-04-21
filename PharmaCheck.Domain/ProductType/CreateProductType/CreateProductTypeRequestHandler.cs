using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.CreateProductType;

public sealed class CreateProductTypeRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<CreateProductTypeRequestHandler> logger)
    : IRequestHandler<CreateProductTypeRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductTypeRequest request, CancellationToken cancellationToken)
    {
        ProductTypeRepository repository = repositoryFactory.NewProductTypeRepository();
        ProductTypeEntity entity = new ProductTypeEntity()
        {
            CategoryId = request.CategoryId,
            Name = request.Name
        };

        try
        {
            await repository.Create(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Can't create product type [{request.Name}] of [{request.CategoryId}] category: {ex.Message}");
            return Result<Guid>.Error("Can't create product type.", ResultErrorStatusCode.InternalError);
        }
        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Created);
    }
}
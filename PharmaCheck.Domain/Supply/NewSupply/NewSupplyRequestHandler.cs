using MediatR;
using Microsoft.Extensions.Logging;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.NewSupply;

public sealed class NewSupplyRequestHandler(
    IRepositoryFactory repositoryFactory,
    ILogger<NewSupplyRequestHandler> logger)
    : IRequestHandler<NewSupplyRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(NewSupplyRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository repository = repositoryFactory.NewSupplyRepository();
        SupplyEntity entity = new SupplyEntity() { SupplierId = request.SupplierId };

        try
        {
            await repository.Create(entity);
        }
        catch (Exception ex)
        {
            logger.LogError($"Can't create supply error: {ex.Message}");
            return Result<Guid>.Error("Can't create supply.", ResultErrorStatusCode.InternalError);
        }

        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Created);
    }
}
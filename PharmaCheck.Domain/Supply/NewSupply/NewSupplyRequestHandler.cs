using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.NewSupply;

public sealed class NewSupplyRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<NewSupplyRequest, Result<Guid>>
{
    private const string PayCheckError = "Check paying error.";

    public async Task<Result<Guid>> Handle(NewSupplyRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository repository = repositoryFactory.NewSupplyRepository();
        SupplyEntity entity = new SupplyEntity() 
        { 
            SupplierId = request.SupplierId,
            PharmacyId = request.PharmacyId,
        };

        try
        {
            await repository.Create(entity);
        }
        catch
        {
            return Result<Guid>.Error(PayCheckError, ResultErrorStatusCode.InternalError);
        }

        return Result<Guid>.Ok(entity.Id, ResultSuccessStatusCode.Created);
    }
}
using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supply.Apply;

public sealed class ApplySupplyRequestHandler(
    IRepositoryFactory factory)
    : IRequestHandler<ApplySupplyRequest, Result>
{
    private const string SupplyNotFoundError = "Supply not found.";
    private const string ApplyError = "Error while apply supply.";

    public async Task<Result> Handle(ApplySupplyRequest request, CancellationToken cancellationToken)
    {
        SupplyRepository supplyRepository = factory.NewSupplyRepository();
        SupplyEntity? supply = await supplyRepository.GetById(request.Id);
        if (supply is null)
        {
            return Result.Error(SupplyNotFoundError, ResultErrorStatusCode.NotFound);
        }

        PharmacyProductsRepository repository = factory.NewPharmacyProductsRepository();

        try
        {
            await repository.ApplyRange(supply.PharmacyId, supply.Products.Select(x => (x.ProductId, x.Count)));
        }
        catch (Exception ex)
        {
            return Result.Error(ApplyError, ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
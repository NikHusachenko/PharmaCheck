using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.AttachToCheck;

public sealed class AttachToCheckRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<AttachToCheckRequest, Result>
{
    private const string CheckOrProductNotFoundError = "Check or product not found.";
    private const string ProductNotAvailableError = "Product not available.";
    private const string ReservingError = "Error while product appends to check.";

    public async Task<Result> Handle(AttachToCheckRequest request, CancellationToken cancellationToken)
    {
        CheckRepository checkRepository = repositoryFactory.NewCheckRepository();
        CheckEntity? dbRecord = await checkRepository.GetById(request.CheckId);
        if (dbRecord is null)
        {
            return Result.Error(CheckOrProductNotFoundError, ResultErrorStatusCode.NotFound);
        }

        PharmacyProductsRepository pharmacyProductsRepository = repositoryFactory.NewPharmacyProductsRepository();

        Result isExists = await IsExists(pharmacyProductsRepository, dbRecord.PharmacyId, request.ProductId);
        if (isExists.IsError)
        {
            return isExists;
        }

        Result reservingResult = await Reserve(pharmacyProductsRepository, dbRecord.PharmacyId, request.ProductId);
        if (reservingResult.IsError)
        {
            return reservingResult;
        }

        ProductCheckRepository productCheckRepository = repositoryFactory.NewProductCheckRepository();
        try
        {
            await productCheckRepository.Attach(request.CheckId, request.ProductId);
        }
        catch
        {
            return Result.Error(CheckOrProductNotFoundError, ResultErrorStatusCode.NotFound);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }

    private async Task<Result> IsExists(PharmacyProductsRepository repository, Guid pharmacyId, Guid productId)
    {
        int count = await repository.GetExists(pharmacyId, productId);

        return count <= 0 ?
            Result.Error(ProductNotAvailableError, ResultErrorStatusCode.NotFound) :
            Result.Ok(ResultSuccessStatusCode.NoContent);
    }

    private async Task<Result> Reserve(PharmacyProductsRepository repository, Guid pharmacyId, Guid productId)
    {
        PharmacyProductsEntity? dbRecord = await repository.Get(pharmacyId, productId);

        if (dbRecord is null)
        {
            return Result.Error(ProductNotAvailableError, ResultErrorStatusCode.NotFound);
        }

        dbRecord.Reserved++;

        try
        {
            await repository.Update(dbRecord);
        }
        catch
        {
            return Result.Error(ReservingError, ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.Pay;

public sealed class PayCheckRequestHandler(
    IRepositoryFactory factory)
    : IRequestHandler<PayCheckRequest, Result>
{
    private const string CheckNotFoundError = "Check not found.";
    private const string MarkCheckIsPaidError = "Error while pay check.";
    private const string CantPayEmptyCheckError = "Can't pay empty check.";

    public async Task<Result> Handle(PayCheckRequest request, CancellationToken cancellationToken)
    {
        CheckRepository repository = factory.NewCheckRepository();

        CheckEntity? entity = await repository.GetById(request.Id);
        if (entity is null)
        {
            return Result.Error(CheckNotFoundError, ResultErrorStatusCode.NotFound);
        }

        if (!entity.Products.Any())
        {
            return Result.Error(CantPayEmptyCheckError, ResultErrorStatusCode.BadRequest);
        }

        PharmacyProductsRepository pharmacyProductsRepository = factory.NewPharmacyProductsRepository();


        entity.PaidAt = DateTimeOffset.Now.ToUniversalTime();
        try
        {
            await repository.Update(entity);
        }
        catch
        {
            return Result.Error(MarkCheckIsPaidError, ResultErrorStatusCode.InternalError);
        }

        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
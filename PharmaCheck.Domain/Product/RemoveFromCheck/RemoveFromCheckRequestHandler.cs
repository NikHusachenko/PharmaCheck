using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.RemoveFromCheck;

public sealed class RemoveFromCheckRequestHandler(
    IRepositoryFactory factory)
    : IRequestHandler<RemoveFromCheckRequest, Result>
{
    private const string ProductNotAttachedToCheckError = "Product not attached to check.";
    private const string CantDetachProductFromPaidCheckError = "Can't remove product from paid check.";
    private const string ErrorWhileDetaching = "Error while detaching.";

    public async Task<Result> Handle(RemoveFromCheckRequest request, CancellationToken cancellationToken)
    {
        ProductCheckRepository repository = factory.NewProductCheckRepository();

        ProductCheckEntity? entity = await repository.Get(request.ProductId, request.CheckId);
        if (entity is null)
        {
            return Result.Error(ProductNotAttachedToCheckError, ResultErrorStatusCode.NotFound);
        }

        if (entity.Check.PaidAt.HasValue)
        {
            return Result.Error(CantDetachProductFromPaidCheckError, ResultErrorStatusCode.BadRequest);
        }

        try
        {
            await repository.Detach(entity);
        }
        catch
        {
            return Result.Error(ErrorWhileDetaching, ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
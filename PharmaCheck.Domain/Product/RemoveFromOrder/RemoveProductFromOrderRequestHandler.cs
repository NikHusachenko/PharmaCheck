using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.RemoveFromOrder;

public sealed class RemoveProductFromOrderRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<RemoveProductFromOrderRequest, Result>
{
    private const string NotFoundProductInOrderError = "Selected order not ordered.";
    private const string CantRemoveProductFromOrderError = "Can't remove product from supply.";

    public async Task<Result> Handle(RemoveProductFromOrderRequest request, CancellationToken cancellationToken)
    {
        ProductSupplyRepository repository = repositoryFactory.NewProductSupplyRepository();

        ProductSuppliesEntity? entity = await repository.Get(request.ProductId, request.OrderId);
        if (entity is null)
        {
            return Result.Error(NotFoundProductInOrderError, ResultErrorStatusCode.NotFound);
        }

        try
        {
            await repository.Remove(entity);
        }
        catch
        {
            return Result.Error(CantRemoveProductFromOrderError, ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.AppendToOrder;

public sealed class AppendProductToOrderRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<AppendProductToOrderRequest, Result>
{
    private const string AppendError = "Error while order the product.";

    public async Task<Result> Handle(AppendProductToOrderRequest request, CancellationToken cancellationToken)
    {
        ProductSupplyRepository repository = repositoryFactory.NewProductSupplyRepository();

        try
        {
            await repository.Append(new ProductSuppliesEntity()
            {
                Count = request.Count,
                ProductId = request.ProductId,
                SupplyId = request.SupplyId
            });
        }
        catch
        {
            return Result.Error(AppendError, ResultErrorStatusCode.InternalError);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
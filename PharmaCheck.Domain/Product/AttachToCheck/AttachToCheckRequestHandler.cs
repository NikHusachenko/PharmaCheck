using MediatR;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.AttachToCheck;

public sealed class AttachToCheckRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<AttachToCheckRequest, Result>
{
    private const string CheckOrProductNotFoundError = "Check or product not found.";

    public async Task<Result> Handle(AttachToCheckRequest request, CancellationToken cancellationToken)
    {
        ProductCheckRepository repository = repositoryFactory.NewProductCheckRepository();

        try
        {
            await repository.Attach(request.CheckId, request.ProductId);
        }
        catch
        {
            return Result.Error(CheckOrProductNotFoundError, ResultErrorStatusCode.NotFound);
        }
        return Result.Ok(ResultSuccessStatusCode.NoContent);
    }
}
using MediatR;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.NewProduct;

public sealed class NewProductRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<NewProductRequest, Result<Guid>>
{
    public Task<Result<Guid>> Handle(NewProductRequest request, CancellationToken cancellationToken)
    {
        throw new Exception();
    }
}
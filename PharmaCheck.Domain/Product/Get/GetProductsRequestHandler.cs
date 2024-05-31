using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;

namespace PharmaCheck.Domain.Product.Get;

public sealed class GetProductsRequestHandler(
    IRepositoryFactory factory) 
    : IRequestHandler<GetProductsRequest, List<ProductEntity>>
{
    public async Task<List<ProductEntity>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        ProductRepository repository = factory.NewProductRepository();
        return await repository.Get(request.PharmacyId, request.Name, request.Description, request.Manufacturer, request.MinPrice, request.MaxPrice, request.Page);
    }
}
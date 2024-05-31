using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Product.NewProduct;

public sealed class NewProductRequestHandler(IRepositoryFactory factory)
    : IRequestHandler<NewProductRequest, Result<ProductEntity>>
{
    private const string ErrorWhileCreate = "Error while create product.";

    public async Task<Result<ProductEntity>> Handle(NewProductRequest request, CancellationToken cancellationToken)
    {
        ProductRepository repository = factory.NewProductRepository();
        ProductEntity entity = new ProductEntity()
        {
            Description = request.Description,
            Name = request.Name,
            Manufacturer = request.Manufacturer,
            Price = request.Price,
            TypeId = request.ProductTypeId,
        };

        try
        {
            await repository.Create(entity);
        }
        catch
        {
            return Result<ProductEntity>.Error(ErrorWhileCreate, ResultErrorStatusCode.InternalError);
        }
        return Result<ProductEntity>.Ok(entity, ResultSuccessStatusCode.Ok);
    }
}
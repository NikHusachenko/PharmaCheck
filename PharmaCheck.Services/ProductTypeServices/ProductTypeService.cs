using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Services.ProductTypeServices;

public sealed class ProductTypeService : IProductTypeService
{
    private readonly ProductTypeRepository _productTypeRepository;

    public ProductTypeService(IRepositoryFactory repositoryFactory)
    {
        _productTypeRepository = repositoryFactory.NewProductTypeRepository();
    }

    public async Task<Result<Guid>> Create(string name)
    {
        throw new NotImplementedException();
    }
}
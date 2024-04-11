using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;

namespace PharmaCheck.Services.ProductTypeServices;

public sealed class ProductTypeService : IProductTypeService
{
    private readonly ProductTypeRepository _productTypeRepository;

    public ProductTypeService(IRepositoryFactory repositoryFactory)
    {
        _productTypeRepository = repositoryFactory.NewProductTypeRepository();
    }
}
using PharmaCheck.Services.Response;

namespace PharmaCheck.Services.ProductTypeServices;

public interface IProductTypeService
{
    Task<Result<Guid>> Create(string name);
}
namespace PharmaCheck.EntityFramework.Repositories.Factories;

public interface IRepositoryFactory
{
    ProductTypeRepository NewProductTypeRepository();
    CategoryRepository NewCategoryRepository();
    SupplyRepository NewSupplyRepository();
    SupplierRepository NewSupplierRepository();
    PharmacyRepository NewPharmacyRepository();
    ProductRepository NewProductRepository();
}
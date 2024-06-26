﻿namespace PharmaCheck.EntityFramework.Repositories.Factories;

public interface IRepositoryFactory
{
    ProductTypeRepository NewProductTypeRepository();
    CategoryRepository NewCategoryRepository();
    SupplyRepository NewSupplyRepository();
    SupplierRepository NewSupplierRepository();
    PharmacyRepository NewPharmacyRepository();
    ProductRepository NewProductRepository();
    UserRepository NewUserRepository();
    CheckRepository NewCheckRepository();
    ProductCheckRepository NewProductCheckRepository();
    PharmacyProductsRepository NewPharmacyProductsRepository();
    ProductSupplyRepository NewProductSupplyRepository();
}
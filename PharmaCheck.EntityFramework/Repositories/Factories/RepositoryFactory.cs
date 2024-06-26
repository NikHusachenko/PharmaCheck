﻿namespace PharmaCheck.EntityFramework.Repositories.Factories;

public sealed class RepositoryFactory : IRepositoryFactory
{
    private readonly ApplicationDbContext _dbContext;

    public RepositoryFactory(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public ProductTypeRepository NewProductTypeRepository() => new(_dbContext);
    public CategoryRepository NewCategoryRepository() => new(_dbContext);
    public SupplyRepository NewSupplyRepository() => new(_dbContext);
    public SupplierRepository NewSupplierRepository() => new(_dbContext);
    public PharmacyRepository NewPharmacyRepository() => new(_dbContext);
    public ProductRepository NewProductRepository() => new(_dbContext);
    public UserRepository NewUserRepository() => new(_dbContext);
    public CheckRepository NewCheckRepository() => new(_dbContext);
    public ProductCheckRepository NewProductCheckRepository() => new(_dbContext);
    public PharmacyProductsRepository NewPharmacyProductsRepository() => new(_dbContext);
    public ProductSupplyRepository NewProductSupplyRepository() => new(_dbContext);
}
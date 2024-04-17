namespace PharmaCheck.EntityFramework.Repositories.Factories;

public sealed class RepositoryFactory : IRepositoryFactory
{
    private readonly ApplicationDbContext _dbContext;

    public RepositoryFactory(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;   
    }

    public ProductTypeRepository NewProductTypeRepository() => new(_dbContext);
    public CategoryRepository NewCategoryRepository() => new(_dbContext);
}
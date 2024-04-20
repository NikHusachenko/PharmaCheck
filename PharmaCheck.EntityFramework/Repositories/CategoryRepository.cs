using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class CategoryRepository : IRepository<CategoryEntity>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<CategoryEntity> _table;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.ProductCategories;
    }

    public async Task Create(CategoryEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now.ToUniversalTime();
        entity.UpdatedAt = DateTime.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(CategoryEntity entity)
    {
        entity.DeletedAt = DateTime.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(CategoryEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CategoryEntity?> GetById(Guid id) => 
        await _table.FirstOrDefaultAsync(entity => entity.Id == id &&
            !entity.DeletedAt.HasValue);

    public async Task<List<CategoryEntity>> GetAll(string query) =>
        await _table.Where(category => category.Name.Contains(query) &&
            !category.DeletedAt.HasValue)
        .ToListAsync();

    public async Task<CategoryEntity?> GetByName(string name) =>
        await _table.FirstOrDefaultAsync(category => category.Name == name &&
            !category.DeletedAt.HasValue);
}
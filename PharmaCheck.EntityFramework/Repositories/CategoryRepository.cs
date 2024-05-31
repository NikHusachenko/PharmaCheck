using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class CategoryRepository
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
        await _table
            .Include(entity => entity.Types)
            .ThenInclude(type => type.Products)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
                !entity.DeletedAt.HasValue);

    public async Task<List<CategoryEntity>> GetAll(int skip, int take, string query) =>
        await _table.Where(category => category.Name.Contains(query) &&
            !category.DeletedAt.HasValue)
            .Skip(skip).Take(take).ToListAsync();

    public async Task<CategoryEntity?> GetByName(string name) =>
        await _table.Include(entity => entity.Types)
            .FirstOrDefaultAsync(category => category.Name == name &&
                !category.DeletedAt.HasValue);
}
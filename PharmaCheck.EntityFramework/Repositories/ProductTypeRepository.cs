using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class ProductTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<ProductTypeEntity> _table;

    public ProductTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.ProductTypes;
    }

    public async Task Create(ProductTypeEntity entity)
    {
        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductTypeEntity?> GetById(Guid id)
    {
        return await _table.FindAsync(id);
    }
}
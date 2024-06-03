using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class ProductSupplyRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<ProductSuppliesEntity> _table;

    public ProductSupplyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.ProductSupplies;
    }

    public async Task Append(ProductSuppliesEntity entity)
    {
        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Remove(ProductSuppliesEntity entity)
    {
        _table.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductSuppliesEntity?> Get(Guid productId, Guid supplyId) =>
        await _table.FirstOrDefaultAsync(
            entity => entity.ProductId == productId &&
            entity.SupplyId == supplyId);
}
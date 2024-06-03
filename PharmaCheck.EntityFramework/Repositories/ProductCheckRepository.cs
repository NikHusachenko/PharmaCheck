using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class ProductCheckRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<ProductCheckEntity> _table;

    public ProductCheckRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.ProductChecks;
    }

    public async Task Attach(Guid CheckId, Guid ProductId)
    {
        await _table.AddAsync(new ProductCheckEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow.ToUniversalTime(),
            UpdatedAt = DateTimeOffset.UtcNow.ToUniversalTime(),
            Id = Guid.NewGuid(),
            CheckId = CheckId,
            ProductId = ProductId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task Detach(ProductCheckEntity entity)
    {
        _table.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<ProductCheckEntity?> Get(Guid id) =>
        _table.FirstOrDefaultAsync(entity => entity.Id == id &&
            !entity.DeletedAt.HasValue);

    public Task<ProductCheckEntity?> Get(Guid productId, Guid checkId) =>
        _table.Include(pc => pc.Product)
            .Include(pc => pc.Check)
            .FirstOrDefaultAsync(entity => entity.ProductId == productId &&
            entity.CheckId == checkId &&
            !entity.DeletedAt.HasValue);
}
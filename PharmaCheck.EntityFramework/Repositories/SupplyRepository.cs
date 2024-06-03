using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class SupplyRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<SupplyEntity> _table;

    public SupplyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Supplies;
    }

    public async Task Create(SupplyEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(SupplyEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SupplyEntity?> GetById(Guid id) =>
        await _table
            .Include(entity => entity.Products)
            .ThenInclude(supply => supply.Product)
                .ThenInclude(product => product.ProductType)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
                !entity.DeletedAt.HasValue);

    public async Task<List<SupplyEntity>> GetAll(Guid supplierId) =>
        await _table
            .Include(entity => entity.Products)
            .ThenInclude(supply => supply.Product)
                .ThenInclude(product => product.ProductType)
            .Include(entity => entity.Products)
            .ThenInclude(supply => supply.Product)
                .ThenInclude(product => product.Pharmacies)
                    .ThenInclude(pp => pp.Pharmacy)
            .Where(entity => entity.SupplierId == supplierId &&
                !entity.DeletedAt.HasValue)
            .ToListAsync();
}
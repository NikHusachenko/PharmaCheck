using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;
using System.Data;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class PharmacyProductsRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<PharmacyProductsEntity> _table;

    public PharmacyProductsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.PharmacyProducts;
    }

    public async Task AppendOrCreate(Guid pharmacyId, Guid productId, int count)
    {
        PharmacyProductsEntity? entity = await _table.FirstOrDefaultAsync(
            entity => entity.PharmacyId == pharmacyId &&
            entity.ProductId == productId &&
            !entity.DeletedAt.HasValue);

        if (entity is null)
        {
            entity = new PharmacyProductsEntity()
            {
                Count = count,
                CreatedAt = DateTimeOffset.UtcNow.ToUniversalTime(),
                Id = Guid.NewGuid(),
                PharmacyId = pharmacyId,
                ProductId = productId,
                UpdatedAt = DateTimeOffset.UtcNow.ToUniversalTime()
            };

            await _table.AddAsync(entity);
        }
        else
        {
            entity.Count += count;
            _table.Update(entity);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CheckCount(Guid pharmacyId, Guid productId)
    {
        PharmacyProductsEntity? entity = await _table.FirstOrDefaultAsync(
            entity => entity.PharmacyId == productId &&
            entity.ProductId == productId &&
            !entity.DeletedAt.HasValue);

        return entity is null ? -1 : entity.Count;
    }

    public async Task ApplyRange(Guid pharmacyId, IEnumerable<(Guid id, int count)> products)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            var productIds = products.Select(p => p.id).ToList();

            var entities = await _table
                .Where(entity => entity.PharmacyId == pharmacyId && productIds.Contains(entity.ProductId))
                .ToListAsync();

            foreach (var entity in entities)
            {
                var product = products.FirstOrDefault(p => p.id == entity.ProductId);
                if (product != default)
                {
                    entity.Count = product.count;
                }
            }

            await _dbContext.BulkInsertOrUpdateAsync(entities);
            await _dbContext.SaveChangesAsync();

            var existingProductIds = entities.Select(e => e.ProductId).ToHashSet();
            var newProducts = products.Where(p => !existingProductIds.Contains(p.id))
                                      .Select(p => new PharmacyProductsEntity
                                      {
                                          CreatedAt = DateTimeOffset.Now.ToUniversalTime(),
                                          Id = Guid.NewGuid(),
                                          UpdatedAt = DateTimeOffset.Now.ToUniversalTime(),
                                          PharmacyId = pharmacyId,
                                          ProductId = p.id,
                                          Count = p.count
                                      });

            if (newProducts.Any())
            {
                await _dbContext.BulkInsertOrUpdateAsync(newProducts);
                await _dbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
    }
}
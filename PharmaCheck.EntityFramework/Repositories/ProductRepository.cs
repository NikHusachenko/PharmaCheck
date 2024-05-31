using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class ProductRepository
{
    private const int PAGE_VOLUME = 50;

    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<ProductEntity> _table;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Products;
    }

    public async Task Create(ProductEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(ProductEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ProductEntity entity)
    {
        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ProductEntity>> Get(Guid? pharmacyId, string? name, string? description, string? manufacturer, float? minPrice, float? maxPrice, int Page = 1)
    {
        IQueryable<ProductEntity> query = _table.AsNoTracking();

        query = pharmacyId is not null ?
            query.Where(entity => entity.Pharmacies.Where(pp => pp.PharmacyId == pharmacyId.Value).Any()) :
            query;

        query = name is not null ?
            query.Where(entity => entity.Name.Contains(name)) :
            query;

        query = description is not null ?
            query.Where(entity => entity.Description.Contains(description)) :
            query;

        query = manufacturer is not null ?
            query.Where(entity => entity.Manufacturer == manufacturer) :
            query;

        query = minPrice is not null ?
            query.Where(entity => entity.Price >= minPrice.Value) :
            query;

        query = maxPrice is not null ?
            query.Where(entity => entity.Price <= maxPrice.Value) :
            query;

        int skip = Page <= 1 ? 0 : PAGE_VOLUME * (Page - 1);
        return await query.Skip(skip).Take(PAGE_VOLUME).ToListAsync();
    }
}
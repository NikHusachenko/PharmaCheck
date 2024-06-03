using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class CheckRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<CheckEntity> _table;

    public CheckRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        _table = dbContext.Checks;
    }

    public async Task Create(CheckEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(CheckEntity entity)
    {
        if (entity.PaidAt.HasValue)
        {
            entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();
            _table.Update(entity);
        }
        else
        {
            _table.Remove(entity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CheckEntity>> GetAll(Guid? pharmacyId,
        DateTimeOffset? paidTimeFrom,
        DateTimeOffset? paidTimeTo,
        float? minPrice,
        float? maxPrice,
        List<Guid> products)
    {
        IQueryable<CheckEntity> query = _table.AsNoTracking();

        query = pharmacyId.HasValue ?
            query.Where(entity => entity.PharmacyId == pharmacyId.Value) :
            query;

        query = paidTimeFrom.HasValue ?
            query.Where(entity => entity.PaidAt >= paidTimeFrom) :
            query;

        query = paidTimeTo.HasValue ?
            query.Where(entity => entity.PaidAt <= paidTimeTo) :
            query;

        query = minPrice.HasValue ?
            query.Where(entity => entity.Products.Sum(p => p.Product.Price) >= minPrice) :
            query;

        query = maxPrice.HasValue ?
            query.Where(entity => entity.Products.Sum(p => p.Product.Price) <= maxPrice) :
            query;

        query = products.Any() ?
            query.Where(entity => entity.Products.Any(p => products.Contains(p.ProductId))) :
            query;

        return await query.ToListAsync();
    }

    public async Task<CheckEntity?> GetById(Guid id) =>
        await _table.Include(entity => entity.Products)
            .ThenInclude(cp => cp.Product)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
            !entity.DeletedAt.HasValue);

    public async Task Update(CheckEntity entity)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync();
    }
}
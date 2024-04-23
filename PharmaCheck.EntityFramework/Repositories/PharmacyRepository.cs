using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class PharmacyRepository : IRepository<PharmacyEntity>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<PharmacyEntity> _table;

    public PharmacyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Pharmacies;
    }

    public async Task Create(PharmacyEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(PharmacyEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PharmacyEntity?> GetById(Guid id) =>
        await _table
            .Include(entity => entity.Products)
                .ThenInclude(product => product.Category)
            .Include(entity => entity.Products)
                .ThenInclude(product => product.ProductType)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
                !entity.DeletedAt.HasValue);

    public async Task<PharmacyEntity?> GetByName(string name) =>
        await _table
            .Include(entity => entity.Products)
                .ThenInclude(product => product.Category)
            .Include(entity => entity.Products)
                .ThenInclude(product => product.ProductType)
            .FirstOrDefaultAsync(entity => entity.Name == name &&
                !entity.DeletedAt.HasValue);

    public async Task<PharmacyEntity?> GetByAddress(
        string city,
        string region,
        string street,
        string additionAddress) =>
        await _table
            .Include(entity => entity.Products)
                .ThenInclude(product => product.Category)
            .Include(entity => entity.Products)
                .ThenInclude(product => product.ProductType)
            .FirstOrDefaultAsync(entity =>
                entity.Region == region &&
                entity.City == city &&
                entity.Street == street &&
                entity.AdditionAddress == additionAddress &&
                !entity.DeletedAt.HasValue);

    public async Task<bool> CheckByAddress(
        string city, 
        string region, 
        string street,
        string additionAddress) =>
        (await _table.FirstOrDefaultAsync(entity => 
            entity.Region == region &&
            entity.City == city &&
            entity.Street == street &&
            entity.AdditionAddress == additionAddress &&
            !entity.DeletedAt.HasValue)) is null ? false : true;
}
using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class PharmacyRepository
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
                .ThenInclude(product => product.Product)
                    .ThenInclude(product => product.ProductType)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
                !entity.DeletedAt.HasValue);

    public async Task<PharmacyEntity?> GetByName(string name) =>
        await _table
            .Include(entity => entity.Products)
                .ThenInclude(product => product.Product)
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
                .ThenInclude(product => product.Product)
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
        (await GetByAddress(city, region, street, additionAddress)) is null ? false : true;

    public async Task<List<PharmacyEntity>> GetAll(string cityQuery,
        string regionQuery,
        string streetQuery,
        string additionAddressQuery) =>
        await _table.Where(entity =>
            entity.City.Contains(cityQuery) &&
            entity.Region.Contains(regionQuery) &&
            entity.Street.Contains(streetQuery) &&
            entity.AdditionAddress.Contains(additionAddressQuery))
            .Include(entity => entity.Products)
                .ThenInclude(product => product.Product)
                    .ThenInclude(product => product.ProductType)
                        .ThenInclude(type => type.Category)
        .ToListAsync();

    public async Task<List<PharmacyEntity>> GetByProductId(Guid id) =>
        await _table.Where(entity =>
            entity.Products.FirstOrDefault(pp => pp.ProductId == id) != null &&
            !entity.DeletedAt.HasValue)
        .Include(entity => entity.Products)
            .ThenInclude(product => product.Product)
        .ToListAsync();
}
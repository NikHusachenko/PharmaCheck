using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class SupplierRepository : IRepository<SupplierEntity>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<SupplierEntity> _table;

    public SupplierRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Suppliers;
    }

    public async Task Create(SupplierEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(SupplierEntity entity)
    {
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<SupplierEntity>> GetAll(
        int skip,
        int take,
        string nameQuery,
        string regionQuery,
        string cityQuery,
        string streetQuery,
        string additionAddressQuery,
        string contactPhoneQuery) =>
        await _table.Where(entity =>
            entity.Name.Contains(nameQuery) &&
            entity.Region.Contains(regionQuery) &&
            entity.City.Contains(cityQuery) &&
            entity.Street.Contains(streetQuery) &&
            entity.AdditionAddress.Contains(additionAddressQuery) &&
            entity.ContactPhone.Contains(contactPhoneQuery) &&
            !entity.DeletedAt.HasValue)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

    public async Task<SupplierEntity?> GetById(Guid id) =>
        await _table
            .Include(entity => entity.Supplies)
            .FirstOrDefaultAsync(entity => entity.Id == id &&
                !entity.DeletedAt.HasValue);

    public async Task<SupplierEntity?> GetByName(string name) =>
        await _table
            .Include(entity => entity.Supplies)
                .ThenInclude(supply => supply.Products)
                    .ThenInclude(product => product.Category)
                        .ThenInclude(category => category.Types)
            .FirstOrDefaultAsync(entity => entity.Name == name &&
                !entity.DeletedAt.HasValue);

    public async Task<bool> CheckByAddress(string region, string city, string street, string additionAddress)
    {
        SupplierEntity? entity = await _table.FirstOrDefaultAsync(entity =>
            entity.Region == region &&
            entity.City == city &&
            entity.Street == street &&
            entity.AdditionAddress == additionAddress);

        return entity is null ? false : true;
    }
}
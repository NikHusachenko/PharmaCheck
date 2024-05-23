using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class UserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<UserEntity> _table;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Users;
    }

    public async Task Create(UserEntity entity)
    {
        entity.CreatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.Id = Guid.NewGuid();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(UserEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(UserEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();
        entity.DeletedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<UserEntity>> GetAll() =>
        await _table.Where(entity => !entity.DeletedAt.HasValue)
            .ToListAsync();

    public async Task<UserEntity?> GetById(Guid id) =>
        await _table.FirstOrDefaultAsync(entity => !entity.DeletedAt.HasValue &&
            entity.Id == id);

    public async Task<UserEntity?> GetByLogin(string login) =>
        await _table.FirstOrDefaultAsync(entity => entity.Login == login &&
            !entity.DeletedAt.HasValue);
}
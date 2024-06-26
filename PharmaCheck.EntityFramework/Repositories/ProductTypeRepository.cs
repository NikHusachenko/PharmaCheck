﻿using Microsoft.EntityFrameworkCore;
using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public sealed class ProductTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<ProductTypeEntity> _table;

    public ProductTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.ProductTypes;
    }

    public async Task Create(ProductTypeEntity entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.Now.ToUniversalTime();
        entity.UpdatedAt = DateTime.Now.ToUniversalTime();

        await _table.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(ProductTypeEntity entity)
    {
        entity.DeletedAt = DateTime.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ProductTypeEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.Now.ToUniversalTime();

        _table.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductTypeEntity?> GetById(Guid id) =>
        await _table.FirstOrDefaultAsync(entity => entity.Id == id &&
            !entity.DeletedAt.HasValue);

    public async Task<ProductTypeEntity?> GetByName(string name) =>
        await _table.FirstOrDefaultAsync(entity => entity.Name == name &&
            !entity.DeletedAt.HasValue);

    public async Task<List<ProductTypeEntity>> GetAll(int skip, int take, string? queryName, Guid? categoryId)
    {
        IQueryable<ProductTypeEntity> query = _table.AsNoTracking();

        query = queryName is not null ?
            query.Where(entity => entity.Name.Contains(queryName)) :
            query;

        query = categoryId.HasValue ?
            query.Where(entity => entity.CategoryId == categoryId.Value) :
            query;

        return await query.Skip(skip).Take(take).ToListAsync();
    }
}
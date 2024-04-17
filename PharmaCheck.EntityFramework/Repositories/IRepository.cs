using PharmaCheck.Database.Entities;

namespace PharmaCheck.EntityFramework.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task Create(T entity);
    Task Delete(T entity);
    Task<T?> GetById(Guid id);
}
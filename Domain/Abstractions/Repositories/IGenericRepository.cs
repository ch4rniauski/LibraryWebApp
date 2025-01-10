using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<EntityEntry<T>?> Create(T entity);
        Task<T?> GetById(Guid id);
        Task<List<T>?> GetAll();
        EntityEntry<T>? Delete(T entity);
    }
}

using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.DataContext.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LibraryContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LibraryContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<EntityEntry<T>?> Create(T entity)
        {
            var createdEntity = await _dbSet.AddAsync(entity);

            return createdEntity;
        }

        public EntityEntry<T>? Delete(T entity)
        {
            var deletedEntity = _dbSet.Remove(entity);

            return deletedEntity;
        }

        public async Task<List<T>?> GetAll()
        {
            var list = await _dbSet.ToListAsync();

            return list;
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }
    }
}

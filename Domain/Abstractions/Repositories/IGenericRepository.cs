namespace Domain.Abstractions.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<T?> GetById(Guid id);
        Task<List<T>?> GetAll();
        bool Delete(T entity);
    }
}

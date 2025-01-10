using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetByLogin(string login);
        Task<UserEntity?> GetByEmail(string email);
    }
}

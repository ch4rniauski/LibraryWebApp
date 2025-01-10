using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetById(Guid id);
        Task<UserEntity?> GetByLogin(string login);
        Task<UserEntity?> GetByEmail(string email);
    }
}

using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task RegisterUser(UserEntity user);
        Task DeleteUser(Guid id);
    }
}

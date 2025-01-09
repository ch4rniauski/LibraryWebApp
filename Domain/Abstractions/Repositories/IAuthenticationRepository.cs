using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<EntityEntry<UserEntity>?> RegisterUser(UserEntity user);
        Task<EntityEntry<UserEntity>?> DeleteUser(Guid id);
    }
}

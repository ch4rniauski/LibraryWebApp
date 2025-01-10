using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository : IGenericRepository<UserEntity>
    {
    }
}

using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<bool> RegisterUser(UserRecord user);
        Task<string?> LogInUser(UserRecord user);
    }
}

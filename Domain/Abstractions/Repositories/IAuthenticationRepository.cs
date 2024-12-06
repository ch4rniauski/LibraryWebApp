using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        void RegisterUser(UserRecord user);
        Task<string?> LogInUser(UserRecord user);
    }
}

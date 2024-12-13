using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string?> RegisterUser(UserRecord user);
        Task<LogInResponseRecord?> LogInUser(UserRecord user);
    }
}

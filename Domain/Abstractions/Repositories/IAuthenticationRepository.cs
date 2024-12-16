using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string?> RegisterUser(UserRecord user);
        Task<LogInResponseRecord?> LogInUser(UserRecord user, HttpContext context);
        Task<bool> DeleteUser(Guid id);
        Task<string?> UpdateAccessToken(Guid id, string refreshToken);
    }
}

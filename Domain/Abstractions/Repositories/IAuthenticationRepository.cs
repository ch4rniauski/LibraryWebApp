using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string?> RegisterUser(RegisterUserRecord user);
        Task<LogInResponseRecord?> LogInUser(LogInUserRecord user, HttpContext context);
        Task<bool> DeleteUser(Guid id);
        Task<string?> UpdateAccessToken(Guid id, string refreshToken);
    }
}

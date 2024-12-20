using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task RegisterUser(RegisterUserRecord user);
        Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context);
        Task DeleteUser(Guid id);
        Task<string> UpdateAccessToken(Guid id, string refreshToken);
    }
}

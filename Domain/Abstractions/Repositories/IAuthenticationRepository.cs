using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string?> RegisterUser(UserRecord user);
        Task<LogInResponseRecord?> LogInUser(UserRecord user, HttpContext context);
    }
}

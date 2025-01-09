using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Services
{
    public interface IAuthenticationUserService
    {
        Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context);
        Task RegisterUser(RegisterUserRecord user);
        Task DeleteUser(Guid id);
        Task<string> UpdateAccessToken(Guid id, HttpContext context);
    }
}

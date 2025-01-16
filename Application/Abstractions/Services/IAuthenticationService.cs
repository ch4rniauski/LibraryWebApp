using Application.Abstractions.Records;
using Application.Abstractions.Requests;

namespace Application.Abstractions.Services
{
    public interface IAuthenticationUserService
    {
        Task<LogInResponseRecord> LogInUser(LogInRequest user);
        Task RegisterUser(RegisterUserRecord user);
        Task DeleteUser(Guid id);
        Task<string> UpdateAccessToken(Guid id, string? refreshToken);
    }
}

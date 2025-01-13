using Domain.Entities;

namespace Domain.Abstractions.JWT
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(UserEntity user);
        string GenerateRefreshToken();
    }
}

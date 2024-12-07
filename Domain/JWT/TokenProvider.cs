using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Domain.JWT
{
    public class TokenProvider
    {
        private readonly JWTSettings _settings;

        public TokenProvider(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string CreateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creditionals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Dictionary<string, object>
            {
                ["Email"] = user.Email,
                ["Login"] = user.Login
            };

            var descriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                SigningCredentials = creditionals,
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes)
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(descriptor);

            return token;
        }
    }
}

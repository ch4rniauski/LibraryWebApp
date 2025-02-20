﻿using Domain.Abstractions.JWT;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Library.DataContext.JWT
{
    public class TokenProvider : ITokenProvider
    {
        private readonly JWTSettings _settings;

        public TokenProvider(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateAccessToken(UserEntity user)
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

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}

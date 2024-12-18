using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.JWT;
using Library.DataContext;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly LibraryContext _db;
        private readonly TokenProvider _tokenProvider;

        public AuthenticationRepository(LibraryContext db, TokenProvider tokenProvider)
        {
            _db = db;
            _tokenProvider = tokenProvider;
        }

        public async Task<LogInResponseRecord?> LogInUser(LogInRequest user, HttpContext context)
        {
            var userEntity = await _db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (userEntity is null)
                return null;

            var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            var accessToken = _tokenProvider.GenerateAccessToken(userEntity);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            userEntity.RefreshToken = refreshToken;
            userEntity.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(3);

            context.Response.Cookies.Append("accessToken", accessToken);
            context.Response.Cookies.Append("refreshToken", refreshToken);

            if (userEntity.IsAdmin)
                context.Response.Cookies.Append("admin", "true");

            return new LogInResponseRecord(userEntity.Id ,accessToken, refreshToken);
        }

        public async Task<string?> RegisterUser(RegisterUserRecord user)
        {
            var isUserExist = await _db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (isUserExist is not null)
                return "User with that login already exists";
            else
                isUserExist = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (isUserExist is not null)
                return "User with that email already exists";

            var userEntity = user.Adapt<UserEntity>();

            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = passwordHash;
            
            if (user.IsAdmin == "true")
                userEntity.IsAdmin = true;
            else
                userEntity.IsAdmin = false;

            var createdUser = await _db.Users.AddAsync(userEntity);

            if (createdUser is null)
                return "User wasn't registered";
            return null;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return false;

            _db.Users.Remove(user);
            return true;
        }

        public async Task<string?> UpdateAccessToken(Guid id, string refreshToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                return null;

            string accessToken = _tokenProvider.GenerateAccessToken(user);
            return accessToken;
        }
    }
}

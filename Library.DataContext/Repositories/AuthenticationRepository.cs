using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.JWT;
using Library.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly LibraryContext _db;
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;

        public AuthenticationRepository(LibraryContext db, TokenProvider tokenProvider, IMapper mapper)
        {
            _db = db;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }

        public async Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context)
        {
            var userEntity = await _db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (userEntity is null)
                throw new Exception("User with that logn wasn't found");

            if (userEntity.Email != user.Email)
                throw new Exception("User with that Email wasn't found");

            var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Incorrect password");

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

        public async Task RegisterUser(RegisterUserRecord user)
        {
            var isUserExist = await _db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (isUserExist is not null)
                throw new Exception("User with that login already exists");
            
            isUserExist = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (isUserExist is not null)
                throw new Exception("User with that email already exists");

            var userEntity = _mapper.Map<UserEntity>(user);

            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = passwordHash;
            
            if (user.IsAdmin == "true")
                userEntity.IsAdmin = true;
            else
                userEntity.IsAdmin = false;

            var createdUser = await _db.Users.AddAsync(userEntity);

            if (createdUser is null)
                throw new Exception("User wasn't registered");
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new Exception("User with that ID wasn't found");

            _db.Users.Remove(user);
        }

        public async Task<string> UpdateAccessToken(Guid id, string refreshToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new Exception("Either user with that ID doesn't exist or refresh token has expired");

            string accessToken = _tokenProvider.GenerateAccessToken(user);

            return accessToken;
        }
    }
}

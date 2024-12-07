﻿using Accounts.DataContext;
using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.JWT;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AccountsContext _db;
        private readonly TokenProvider _tokenProvider;

        public AuthenticationRepository(AccountsContext db, TokenProvider tokenProvider)
        {
            _db = db;
            _tokenProvider = tokenProvider;
        }

        public async Task<LogInResponseRecord?> LogInUser(UserRecord user)
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

            return new LogInResponseRecord(accessToken, refreshToken);
        }

        public async Task<bool> RegisterUser(UserRecord user)
        {
            var userEntity = user.Adapt<UserEntity>();

            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = passwordHash;

            var createdUser = await _db.Users.AddAsync(userEntity);

            if (createdUser is null)
                return false;
            return true;
        }
    }
}

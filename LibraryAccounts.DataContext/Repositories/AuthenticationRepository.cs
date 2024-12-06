using Accounts.DataContext;
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
        private readonly TokenProvider _provider;

        public AuthenticationRepository(AccountsContext db, TokenProvider provider)
        {
            _db = db;
            _provider = provider;
        }

        public async Task<string?> LogInUser(UserRecord user)
        {
            var userEntity = await _db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (userEntity is null)
                return null;

            var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return _provider.CreateToken(userEntity);
        }

        public void RegisterUser(UserRecord user)
        {
            var userEntity = user.Adapt<UserEntity>();

            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = passwordHash;
        }
    }
}

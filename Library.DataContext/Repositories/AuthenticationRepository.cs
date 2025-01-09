using Domain.Abstractions.Repositories;
using Domain.Entities;
using Library.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryAccounts.DataContext.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly LibraryContext _db;

        public AuthenticationRepository(LibraryContext db)
        {
            _db = db;
        }

        public async Task<EntityEntry<UserEntity>?> RegisterUser(UserEntity user)
        {
            var registeredUser = await _db.Users.AddAsync(user);

            return registeredUser;
        }

        public async Task<EntityEntry<UserEntity>?> DeleteUser(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            var isDeleted = _db.Users.Remove(user!);

            return isDeleted;
        }
    }
}

using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.JWT;
using Library.DataContext;
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

        public async Task RegisterUser(UserEntity user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            _db.Users.Remove(user!);
        }
    }
}

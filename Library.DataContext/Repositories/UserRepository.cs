using Domain.Abstractions.Repositories;
using Domain.Entities;
using Library.DataContext;
using Library.DataContext.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly LibraryContext _db;

        public UserRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UserEntity?> GetByLogin(string login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(a => a.Login == login);

            return user;
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(a => a.Email == email);

            return user;
        }

        public override async Task<UserEntity?> GetById(Guid id)
        {
            var user = await _db.Users.
                Include(u => u.Books).
                FirstOrDefaultAsync(a => a.Id == id);

            return user;
        }
    }
}

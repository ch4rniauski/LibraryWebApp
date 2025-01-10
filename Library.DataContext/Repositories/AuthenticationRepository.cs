using Domain.Abstractions.Repositories;
using Domain.Entities;
using Library.DataContext;
using Library.DataContext.Repositories;

namespace LibraryAccounts.DataContext.Repositories
{
    public class AuthenticationRepository : GenericRepository<UserEntity>, IAuthenticationRepository
    {
        private readonly LibraryContext _db;

        public AuthenticationRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }
    }
}

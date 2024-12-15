using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Library.DataContext;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _db;

        public UserRepository(LibraryContext db)
        {
            _db = db;
        }

        public async Task<UserInfoResponse?> GetUserInfo(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is not null)
                return user.Adapt<UserInfoResponse>();
            return null;
        }
    }
}

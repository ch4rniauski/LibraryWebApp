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

        public async Task<bool> BorrowBook(Guid userId, Guid bookId)
        {
            var user = await _db.Users
                .Include(u => u.Books)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return false;

            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book is null)
                return false;

            book.TakenAt = DateOnly.FromDateTime(DateTime.UtcNow);
            book.DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
            book.UserId = user.Id;
            book.User = user;

            user.Books.Add(book);

            return true;
        }
    }
}

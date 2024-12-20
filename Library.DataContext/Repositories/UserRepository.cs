using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Library.DataContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounts.DataContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public UserRepository(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserInfoResponse> GetUserInfo(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new Exception("User with that ID doesn't exist");
            return _mapper.Map<UserInfoResponse>(user);
        }

        public async Task BorrowBook(Guid userId, Guid bookId)
        {
            var user = await _db.Users
                .Include(u => u.Books)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                throw new Exception("User with that ID doesn't exist");

            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book is null)
                throw new Exception("Book with that ID doesn't exist");

            book.TakenAt = DateOnly.FromDateTime(DateTime.UtcNow);
            book.DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
            book.UserId = user.Id;
            book.User = user;

            user.Books.Add(book);
        }
    }
}

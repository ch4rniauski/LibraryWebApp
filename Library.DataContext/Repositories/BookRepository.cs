using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _db;

        public BookRepository(LibraryContext db)
        {
            _db = db;
        }

        public async Task CreateBook(BookEntity book)
        {
            await _db.Books.AddAsync(book);
        }

        public void DeleteBook(BookEntity book)
        {
            _db.Books.Remove(book);
        }

        public async Task<List<BookEntity>?> GetAllBooks()
        {
            var books = await _db.Books.ToListAsync();

            return books;
        }

        public async Task<BookEntity?> GetBookById(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        public async Task<BookEntity?> GetBookByISBN(string ISBN)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);

            return book;
        }

        public async Task<List<BookEntity>?> GetBooksByUserId(Guid id)
        {
            var books = await _db.Books
                .Where(b => b.UserId == id)
                .ToListAsync();

            return books;
        }
    }
}

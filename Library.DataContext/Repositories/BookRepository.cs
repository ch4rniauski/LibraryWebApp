using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mapster;
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

        public async Task<bool> CreateBook(BookRecord book)
        {
            BookEntity newBook = book.Adapt<BookEntity>();

            var createdAuthor = await _db.Books.AddAsync(newBook);

            return (createdAuthor is not null);
        }

        public async Task<bool> DeleteBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return false;

            _db.Books.Remove(book);

            return true;
        }

        public List<BookRecord>? GetAllBooks()
        {
            var books = _db.Books;

            if (books is null)
                return null;

            return _db.Books.Adapt<List<BookRecord>>();
        }

        public async Task<BookRecord?> GetBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return null;

            return book.Adapt<BookRecord>();
        }

        public async Task<bool> UpdateBook(BookRecord book)
        {
            var bookToUpdate = await _db.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (bookToUpdate is null)
                return false;

            bookToUpdate = book.Adapt<BookEntity>();

            return true;
        }
    }
}

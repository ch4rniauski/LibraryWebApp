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

        public async Task<bool> CreateBook(CreateBookRecord book)
        {
            BookEntity newBook = book.Adapt<BookEntity>();
            newBook.Id = Guid.NewGuid();

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == book.AuthorFirstName.ToLower());

                if (authorByFirstName is not null)
                    newBook.AuthorId = authorByFirstName.Id;
                else if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                    if (authorBySecondName is not null)
                        newBook.AuthorId = authorBySecondName.Id;
                }
            }
            if (book.AuthorSecondName is not null && newBook.AuthorId is null)
            {
                var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                if (authorBySecondName is not null)
                    newBook.AuthorId = authorBySecondName.Id;
            }

            var createdBook = await _db.Books.AddAsync(newBook);

            return (createdBook is not null);
        }

        public async Task<bool> DeleteBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return false;

            _db.Books.Remove(book);

            return true;
        }

        public List<CreateBookRecord> GetAllBooks()
        {
            return _db.Books.Adapt<List<CreateBookRecord>>();
        }

        public async Task<UpdateBookRecord?> GetBookById(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return null;

            return book.Adapt<UpdateBookRecord>();
        }

        public async Task<UpdateBookRecord?> GetBookByISBN(string ISBN)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);

            if (book is null)
                return null;

            return book.Adapt<UpdateBookRecord>();
        }

        public async Task<bool> UpdateBook(UpdateBookRecord book)
        {
            var bookToUpdate = await _db.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (bookToUpdate is null)
                return false;

            bookToUpdate.ISBN = book.ISBN;
            bookToUpdate.TakenAt = book.TakenAt;
            bookToUpdate.DueDate = book.DueDate;
            bookToUpdate.Description = book.Description;
            bookToUpdate.AuthorFirstName = book.AuthorFirstName;
            bookToUpdate.AuthorSecondName = book.AuthorSecondName;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Id = book.Id;

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == book.AuthorFirstName.ToLower());

                if (authorByFirstName is not null)
                {
                    bookToUpdate.AuthorId = authorByFirstName.Id;
                    isAuthorChanged = true;
                }
                else if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                    if (authorBySecondName is not null)
                    {
                        bookToUpdate.AuthorId = authorBySecondName.Id;
                        isAuthorChanged = true;
                    }
                }
            }
            if (isAuthorChanged == false && book.AuthorSecondName is not null && bookToUpdate.AuthorId is null)
            {
                var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                if (authorBySecondName is not null)
                    bookToUpdate.AuthorId = authorBySecondName.Id;
            }

            return true;
        }
    }
}

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

        public async Task<string?> CreateBook(CreateBookRecord book)
        {
            BookEntity newBook = book.Adapt<BookEntity>();

            newBook.Id = Guid.NewGuid();
            newBook.AuthorFirstName = null;
            newBook.AuthorSecondName = null;

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == book.AuthorFirstName.ToLower());

                if (authorByFirstName is not null)
                {
                    newBook.AuthorId = authorByFirstName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorSecondName is not null && book.AuthorSecondName.ToLower() != authorByFirstName.SecondName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        newBook.AuthorFirstName = authorByFirstName.FirstName;
                        newBook.AuthorSecondName = authorByFirstName.SecondName;
                    }
                }
                if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                    if (authorBySecondName is not null)
                    {
                        newBook.AuthorId = authorBySecondName.Id;
                        isAuthorChanged = true;

                        if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.SecondName.ToLower())
                            isAuthorChanged = false;
                        else
                        {
                            newBook.AuthorFirstName = authorBySecondName.FirstName;
                            newBook.AuthorSecondName = authorBySecondName.SecondName;
                        }
                    }
                }
            }
            if (isAuthorChanged == false && book.AuthorSecondName is not null)
            {
                var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                if (authorBySecondName is not null)
                {
                    newBook.AuthorId = authorBySecondName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.FirstName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        newBook.AuthorFirstName = authorBySecondName.FirstName;
                        newBook.AuthorSecondName = authorBySecondName.SecondName;
                    }
                }
            }

            if ((book.AuthorFirstName is not null || book.AuthorSecondName is not null) && !isAuthorChanged)
                return "Author with that name doesn't exist";

            var createdBook = await _db.Books.AddAsync(newBook);

            return string.Empty;
        }

        public async Task<bool> DeleteBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return false;

            _db.Books.Remove(book);

            return true;
        }

        public List<GetBookRecord>? GetAllBooks()
        {
            var books = _db.Books;

            List<GetBookRecord> booksToReturn = new();

            foreach (var book in books)
            {
                var newBook = new GetBookRecord(
                    book.Id,
                    book.ISBN,
                    book.Title,
                    book.Genre,
                    book.Description,
                    book.AuthorFirstName,
                    book.AuthorSecondName,
                    book.ImageURL,
                    book.UserId);

                booksToReturn.Add(newBook);
            }

            return booksToReturn;
        }

        public async Task<GetBookRecord?> GetBookById(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                return null;

            return new GetBookRecord(
                book.Id,
                book.ISBN,
                book.Title,
                book.Genre,
                book.Description,
                book.AuthorFirstName,
                book.AuthorSecondName,
                book.ImageURL,
                book.UserId);
        }

        public async Task<GetBookRecord?> GetBookByISBN(string ISBN)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);

            if (book is null)
                return null;

            return book.Adapt<GetBookRecord>();
        }

        public List<GetBookRecord>? GetBooksByUserId(Guid id)
        {
            var books = _db.Books.Where(b => b.UserId == id);

            List<GetBookRecord> booksToReturn = new();

            foreach (var book in books)
            {
                var newBook = new GetBookRecord(
                    book.Id,
                    book.ISBN,
                    book.Title,
                    book.Genre,
                    book.Description,
                    book.AuthorFirstName,
                    book.AuthorSecondName,
                    book.ImageURL,
                    book.UserId);

                booksToReturn.Add(newBook);
            }

            return booksToReturn;
        }

        public async Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request)
        {
            List<GetBookResponse>? booksToReturn = new();
            var books = await _db.Books.ToListAsync();

            if (!string.IsNullOrWhiteSpace(request.Search) && books is not null)
            {
                books = await _db.Books
                    .Where(b => b.Title.Contains(request.Search))
                    .ToListAsync();
            }

            if (books is not null)
            {
                books = request.SortBy switch
                {
                    "genre" => books.OrderBy(b => b.Genre).ToList(),
                    "author" => books.OrderBy(b => b.AuthorFirstName).ThenBy(b => b.AuthorSecondName).ToList(),
                    _ => books
                };
            }

            if (books is not null)
            {
                foreach(var book in books)
                    booksToReturn.Add(new GetBookResponse(book.Id, book.Title, book.ImageURL));
            }

            return booksToReturn;
        }

        public async Task<bool> UpdateBook(CreateBookRecord book, Guid id)
        {
            var bookToUpdate = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToUpdate is null)
                return false;

            if (book.AuthorFirstName is null && book.AuthorSecondName is null)
            {
                bookToUpdate.AuthorId = null;
                bookToUpdate.AuthorFirstName = null;
                bookToUpdate.AuthorSecondName = null;
            }

            bookToUpdate.ISBN = book.ISBN;
            bookToUpdate.Description = book.Description;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Id = id;
            bookToUpdate.ImageURL = book.ImageURL;

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == book.AuthorFirstName.ToLower());

                if (authorByFirstName is not null)
                {
                    bookToUpdate.AuthorId = authorByFirstName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorSecondName is not null && book.AuthorSecondName.ToLower() != authorByFirstName.SecondName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        bookToUpdate.AuthorFirstName = authorByFirstName.FirstName;
                        bookToUpdate.AuthorSecondName = authorByFirstName.SecondName;
                    }
                }
                if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                    if (authorBySecondName is not null)
                    {
                        bookToUpdate.AuthorId = authorBySecondName.Id;
                        isAuthorChanged = true;

                        if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.SecondName.ToLower())
                            isAuthorChanged = false;
                        else
                        {
                            bookToUpdate.AuthorFirstName = authorBySecondName.FirstName;
                            bookToUpdate.AuthorSecondName = authorBySecondName.SecondName;
                        }
                    }
                }
            }
            if (isAuthorChanged == false && book.AuthorSecondName is not null)
            {
                var authorBySecondName = await _db.Auhtors.FirstOrDefaultAsync(a => a.SecondName.ToLower() == book.AuthorSecondName.ToLower());

                if (authorBySecondName is not null)
                {
                    bookToUpdate.AuthorId = authorBySecondName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.FirstName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        bookToUpdate.AuthorFirstName = authorBySecondName.FirstName;
                        bookToUpdate.AuthorSecondName = authorBySecondName.SecondName;
                    }
                }
            }

            if ((book.AuthorFirstName is not null || book.AuthorSecondName is not null) && !isAuthorChanged)
                return false;
            return true;
        }
    }
}

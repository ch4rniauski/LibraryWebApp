using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public BookRepository(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task CreateBook(CreateBookRecord book)
        {
            var newBook = _mapper.Map<BookEntity>(book);

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
                throw new Exception("Author with that name doesn't exist");

            await _db.Books.AddAsync(newBook);
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                throw new Exception("Book with that ID wasn't found");

            _db.Books.Remove(book);
        }

        public List<GetBookRecord>? GetAllBooks()
        {
            var books = _db.Books;

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(books);

            return booksToReturn;
        }

        public async Task<GetBookRecord> GetBookById(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                throw new Exception("Book with that ID wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }

        public async Task<GetBookRecord> GetBookByISBN(string ISBN)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);

            if (book is null)
                throw new Exception("Book with that ISBN wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }

        public async Task<List<GetBookRecord>?> GetBooksByUserId(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new Exception("User with that ID doesn't exist");

            var books = _db.Books.Where(b => b.UserId == id);

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(books);

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
                booksToReturn = _mapper.Map<List<GetBookResponse>>(books);

            return booksToReturn;
        }

        public async Task ReturnBook(Guid id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
                throw new Exception("Book with that ID doesn't exist");

            book.UserId = null;
            book.User = null;
            book.TakenAt = null;
            book.DueDate = null;
        }

        public async Task UpdateBook(CreateBookRecord book, Guid id)
        {
            var bookToUpdate = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToUpdate is null)
                throw new Exception("Book with that ID wasn't found");

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
                throw new Exception("Author with that name doesn't exist");
        }
    }
}

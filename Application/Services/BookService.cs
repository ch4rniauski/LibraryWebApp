using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookRecord> _validator;

        public BookService(IUnitOfWork uow, IMapper mapper, IValidator<CreateBookRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task CreateBook(CreateBookRecord book)
        {
            var result = await _validator.ValidateAsync(book);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var newBook = _mapper.Map<BookEntity>(book);

            newBook.Id = Guid.NewGuid();
            newBook.AuthorFirstName = null;
            newBook.AuthorSecondName = null;

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _uow.AuthorRepository.GetByFirstName(book.AuthorFirstName);

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
                    var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

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
                var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

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
                throw new NotFoundException("Author with that name doesn't exist");

            var createdBook = await _uow.BookRepository.Create(newBook);

            if (createdBook is null)
                throw new CreationFailureException("Author wasn't created");

            await _uow.Save();
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID wasn't found");

            var isDeleted = _uow.BookRepository.Delete(book);

            if (isDeleted is null)
                throw new RemovalFailureException("Author with that ID wasn't deleted");

            await _uow.Save();
        }

        public async Task<List<GetBookRecord>?> GetAllBooks()
        {
            var list = await _uow.BookRepository.GetAll();

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(list);

            return booksToReturn;
        }

        public async Task<GetBookRecord> GetBookById(Guid id)
        {
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }

        public async Task<GetBookRecord> GetBookByISBN(string ISBN)
        {
            var book = await _uow.BookRepository.GetBookByISBN(ISBN);

            if (book is null)
                throw new NotFoundException("Book with that ISBN wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }

        public async Task<List<GetBookRecord>?> GetBooksByUserId(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            var books = await _uow.BookRepository.GetBooksByUserId(id);

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(books);

            return booksToReturn;
        }

        public async Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request)
        {
            List<GetBookResponse>? booksToReturn = new();
            var books = await _uow.BookRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Search) && books is not null)
            {
                books = books
                    .Where(b => b.Title.Contains(request.Search))
                    .ToList();
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
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID doesn't exist");

            book.UserId = null;
            book.User = null;
            book.TakenAt = null;
            book.DueDate = null;

            await _uow.Save();
        }

        public async Task UpdateBook(CreateBookRecord book, Guid id)
        {
            var result = await _validator.ValidateAsync(book);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var bookToUpdate = await _uow.BookRepository.GetById(id);

            if (bookToUpdate is null)
                throw new NotFoundException("Book with that ID wasn't found");

            if (book.AuthorFirstName is null && book.AuthorSecondName is null)
            {
                bookToUpdate.AuthorId = null;
                bookToUpdate.AuthorFirstName = null;
                bookToUpdate.AuthorSecondName = null;
            }

            _mapper.Map(book, bookToUpdate);

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _uow.AuthorRepository.GetByFirstName(book.AuthorFirstName);

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
                    var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

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
                var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

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
                throw new NotFoundException("Author with that name doesn't exist");

            await _uow.Save();
        }
    }
}

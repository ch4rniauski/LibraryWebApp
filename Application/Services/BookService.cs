using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Commands.BookCommands;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMediator _mediator;

        public BookService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateBook(CreateBookRecord book)
        {
            var command = new CreateBookCommand { Book = book };

            await _mediator.Send(command);
        }

        public async Task DeleteBook(Guid id)
        {
            var command = new DeleteBookCommand { BookId = id };

            await _mediator.Send(command);
        }

        public async Task<List<GetBookRecord>?> GetAllBooks()
        {
            var query = new GetAllBooksQuery();

            var books = await _mediator.Send(query);

            return books;
        }

        public async Task<GetBookRecord> GetBookById(Guid id)
        {
            var qeury = new GetBookByIdQuery { BookId = id };

            var book = await _mediator.Send(qeury);

            return book;
        }

        public async Task<GetBookRecord> GetBookByISBN(string ISBN)
        {
            var query = new GetBookByISBNQuery { BookISBN = ISBN };

            var book = await _mediator.Send(query);

            return book;
        }

        public async Task<List<GetBookRecord>?> GetBooksByUserId(Guid id)
        {
            var query = new GetBooksByUserIdQuery { UserId = id };

            var list = await _mediator.Send(query);

            return list;
        }

        public async Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request)
        {
            var query = new GetBooksWithParamsQuery { Request = request };

            var books = await _mediator.Send(query);

            return books;
        }

        public async Task ReturnBook(Guid id)
        {
            var command = new ReturnBookCommand { BookId = id };

            await _mediator.Send(command);
        }

        public async Task UpdateBook(CreateBookRecord book, Guid id)
        {
            var command = new UpdateBookCommand
            {
                Book = book,
                BookId = id 
            };

            await _mediator.Send(command);
        }
    }
}

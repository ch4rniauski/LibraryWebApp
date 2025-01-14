using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Abstractions.UseCases.BookUseCases;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly ICreateBookUseCase _createBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIDUseCase;
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;
        private readonly IGetBooksByUserIdUseCase _getBookByUserIdUseCase;
        private readonly IGetBooksWithParamsUseCase _getBooksWithParamsUseCase;
        private readonly IReturnBookUseCase _returnBookUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;

        public BookService(ICreateBookUseCase createBookUseCase,
            IDeleteBookUseCase deleteBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase,
            IGetBookByIdUseCase getBookByIDUseCase,
            IGetBookByISBNUseCase getBookByISBNUseCase,
            IGetBooksByUserIdUseCase getBookByUserIdUseCase,
            IGetBooksWithParamsUseCase getBooksWithParamsUseCase,
            IReturnBookUseCase returnBookUseCase,
            IUpdateBookUseCase updateBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIDUseCase = getBookByIDUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _getBookByUserIdUseCase = getBookByUserIdUseCase;
            _getBooksWithParamsUseCase = getBooksWithParamsUseCase;
            _returnBookUseCase = returnBookUseCase;
            _updateBookUseCase = updateBookUseCase;
        }

        public async Task CreateBook(CreateBookRecord book)
        {
            await _createBookUseCase.Execute(book);
        }

        public async Task DeleteBook(Guid id)
        {
            await _deleteBookUseCase.Execute(id);
        }

        public async Task<List<GetBookRecord>?> GetAllBooks()
        {
            var list = await _getAllBooksUseCase.Execute();

            return list;
        }

        public async Task<GetBookRecord> GetBookById(Guid id)
        {
            var book = await _getBookByIDUseCase.Execute(id);

            return book;
        }

        public async Task<GetBookRecord> GetBookByISBN(string ISBN)
        {
            var book = await _getBookByISBNUseCase.Execute(ISBN);

            return book;
        }

        public async Task<List<GetBookRecord>?> GetBooksByUserId(Guid id)
        {
            var list = await _getBookByUserIdUseCase.Execute(id);

            return list;
        }

        public async Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request)
        {
            var books = await _getBooksWithParamsUseCase.Execute(request);

            return books;
        }

        public async Task ReturnBook(Guid id)
        {
            await _returnBookUseCase.Execute(id);
        }

        public async Task UpdateBook(CreateBookRecord book, Guid id)
        {
            await _updateBookUseCase.Execute(book, id);
        }
    }
}

using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.BookUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ICreateBookUseCase _createBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIdUseCase;
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;
        private readonly IGetBooksByUserIdUseCase _getBooksByUserIdUseCase;
        private readonly IGetBooksWithParamsUseCase _getBooksWithParamsUseCase;
        private readonly IReturnBookUseCase _returnBookUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;

        public BookController(ICreateBookUseCase createBookUseCase,
            IDeleteBookUseCase deleteBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase,
            IGetBookByIdUseCase getBookByIdUseCase,
            IGetBookByISBNUseCase getBookByISBNUseCase,
            IGetBooksByUserIdUseCase getBooksByUserIdUseCase,
            IGetBooksWithParamsUseCase getBooksWithParamsUseCase,
            IReturnBookUseCase returnBookUseCase,
            IUpdateBookUseCase updateBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _getBooksByUserIdUseCase = getBooksByUserIdUseCase;
            _getBooksWithParamsUseCase = getBooksWithParamsUseCase;
            _returnBookUseCase = returnBookUseCase;
            _updateBookUseCase = updateBookUseCase;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateBookRecord request)
        {
            await _createBookUseCase.Execute(request);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetBookRecord>?>> GetAll()
        {
            var list = await _getAllBooksUseCase.Execute();
            
            return Ok(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetBookRecord?>> GetById(Guid id)
        {
            var book = await _getBookByIdUseCase.Execute(id);

            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<GetBookRecord?>> GetByISBN(string isbn)
        {
            var book = await _getBookByISBNUseCase.Execute(isbn);

            return Ok(book);
        }

        [HttpPost("getbooks")]
        public async Task<ActionResult<List<GetBookResponse>?>> GetWithParams([FromBody] GetBookRequest request)
        {
            var books = await _getBooksWithParamsUseCase.Execute(request);

            return Ok(books);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _deleteBookUseCase.Execute(id);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] CreateBookRecord request, Guid id)
        {
            await _updateBookUseCase.Execute(request, id);

            return Ok();
        }

        [HttpGet("borrowed")]
        [Authorize]
        public async Task <ActionResult<List<GetBookRecord>?>> GetBooksByUserId(Guid userId)
        {
            var books = await _getBooksByUserIdUseCase.Execute(userId);

            return Ok(books);
        }

        [HttpPut("return/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(Guid id)
        {
            await _returnBookUseCase.Execute(id);

            return Ok();
        }
    }
}

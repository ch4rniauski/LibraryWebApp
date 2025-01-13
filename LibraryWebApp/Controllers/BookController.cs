using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateBookRecord request)
        {
            await _bookService.CreateBook(request);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetBookRecord>?>> GetAll()
        {
            var list = await _bookService.GetAllBooks();
            
            return Ok(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetBookRecord?>> GetById(Guid id)
        {
            var book = await _bookService.GetBookById(id);

            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<GetBookRecord?>> GetByISBN(string isbn)
        {
            var book = await _bookService.GetBookByISBN(isbn);

            return Ok(book);
        }

        [HttpPost("getbooks")]
        public async Task<ActionResult<List<GetBookResponse>?>> GetWithParams([FromBody] GetBookRequest request)
        {
            var books = await _bookService.GetBooksWithParams(request);

            return Ok(books);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _bookService.DeleteBook(id);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] CreateBookRecord request, Guid id)
        {
            await _bookService.UpdateBook(request, id);

            return Ok();
        }

        [HttpGet("borrowed")]
        [Authorize]
        public async Task <ActionResult<List<GetBookRecord>?>> GetBooksByUserId(Guid userId)
        {
            var books = await _bookService.GetBooksByUserId(userId);

            return Ok(books);
        }

        [HttpPut("return/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(Guid id)
        {
            await _bookService.ReturnBook(id);

            return Ok();
        }
    }
}

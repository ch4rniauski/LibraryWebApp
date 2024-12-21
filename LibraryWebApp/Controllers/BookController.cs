using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateBookRecord> _validator;

        public BookController(IUnitOfWork uow, IValidator<CreateBookRecord> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateBookRecord request)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uow.BookRepository.CreateBook(request);

            _uow.Save();

            return Ok();
        }

        [HttpGet("all")]
        public ActionResult<List<GetBookRecord>?> GetAll()
        {
            return Ok(_uow.BookRepository.GetAllBooks());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetBookRecord?>> GetById(Guid id)
        {
            var book = await _uow.BookRepository.GetBookById(id);

            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<GetBookRecord?>> GetByISBN(string isbn)
        {
            var book = await _uow.BookRepository.GetBookByISBN(isbn);

            return Ok(book);
        }

        [HttpPost("getbooks")]
        public async Task<ActionResult<List<GetBookResponse>?>> GetWithParams([FromBody] GetBookRequest request)
        {
            var books = await _uow.BookRepository.GetBooksWithParams(request);

            return Ok(books);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _uow.BookRepository.DeleteBook(id);

            _uow.Save();

            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] CreateBookRecord request, Guid id)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uow.BookRepository.UpdateBook(request, id);

            _uow.Save();

            return Ok();
        }

        [HttpGet("borrowed")]
        [Authorize]
        public async Task <ActionResult<List<GetBookRecord>?>> GetBooksByUserId(Guid userId)
        {
            var books = await _uow.BookRepository.GetBooksByUserId(userId);

            return Ok(books);
        }

        [HttpPut("return/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(Guid id)
        {
            await _uow.BookRepository.ReturnBook(id);

            _uow.Save();

            return Ok();
        }
    }
}

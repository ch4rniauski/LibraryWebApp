using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IValidator<CreateBookRecord> _validator;

        public BookController(IUnitOfWork uof, IValidator<CreateBookRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBookRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            string? isCreated = await _uof.BookRepository.CreateBook(request);

            if (!string.IsNullOrEmpty(isCreated))
                return BadRequest($"Book wasn't created. {isCreated}");

            _uof.Save();

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<GetBookRecord>> GetAll()
        {
            return Ok(_uof.BookRepository.GetAllBooks());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UpdateBookRecord?>> GetById(Guid id)
        {
            var book = await _uof.BookRepository.GetBookById(id);

            if (book is null)
                return NotFound("Book with that ID wasn't found");
            return Ok(book);
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<UpdateBookRecord?>> GetByISBN(string isbn)
        {
            var book = await _uof.BookRepository.GetBookByISBN(isbn);

            if (book is null)
                return NotFound("Book with that ISBN wasn't found");
            return Ok(book);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool isDeleted = await _uof.BookRepository.DeleteBook(id);

            if (!isDeleted)
                return NotFound("Book with that ID wasn't found");

            _uof.Save();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateBookRecord request)
        {
            var bookToValidate = request.Adapt<CreateBookRecord>();
            var result = await _validator.ValidateAsync(bookToValidate);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            bool isUpdated = await _uof.BookRepository.UpdateBook(request);

            if (!isUpdated)
                return NotFound("Book with that ID wasn't found");

            _uof.Save();

            return Ok();
        }
    }
}

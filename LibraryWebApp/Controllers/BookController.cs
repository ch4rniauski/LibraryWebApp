using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWorkLibrary _uof;
        private readonly IValidator<BookRecord> _validator;

        public BookController(IUnitOfWorkLibrary uof, IValidator<BookRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            bool isCreated = await _uof.BookRepository.CreateBook(request);

            if (!isCreated)
                return BadRequest("Book wasn't created");

            _uof.Save();

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<BookRecord>?> GetAll()
        {
            return Ok(_uof.BookRepository.GetAllBooks());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<BookRecord?>> Get(Guid id)
        {
            var book = await _uof.BookRepository.GetBook(id);

            if (book is null)
                return NotFound("Book with that ID wasn't found");
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
        public async Task<ActionResult> Update([FromBody] BookRecord request)
        {
            var result = await _validator.ValidateAsync(request);

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

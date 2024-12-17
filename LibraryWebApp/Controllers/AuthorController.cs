using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IValidator<CreateAuthorRecord> _validator;

        public AuthorController(IUnitOfWork uof, IValidator<CreateAuthorRecord> validator)
        {
            _uof = uof;
            _validator = validator;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateAuthorRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            bool isCreated = await _uof.AuthorRepository.CreateAuthor(request);

            if (!isCreated)
                return BadRequest("Book wasn't created");

            _uof.Save();

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_uof.AuthorRepository.GetAllAuthors());
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var author = await _uof.AuthorRepository.GetAuthor(id);

            if (author is null)
                return NotFound("Author with that ID wasn't found");
            return Ok(author);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool isDeleted = await _uof.AuthorRepository.DeleteAutor(id);

            if (!isDeleted)
                return NotFound("Author with that ID wasn't found");

            _uof.Save();

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] UpdateAuthorRecord request)
        {
            var authorToValidate = request.Adapt<CreateAuthorRecord>();
            var result = await _validator.ValidateAsync(authorToValidate);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            bool isUpdated = await _uof.AuthorRepository.UpdateAuthor(request);

            if (!isUpdated)
                return NotFound("Author with that ID wasn't found");

            _uof.Save();

            return Ok();
        }
    }
}

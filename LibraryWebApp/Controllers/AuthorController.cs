using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;
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
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork uof, IValidator<CreateAuthorRecord> validator, IMapper mapper)
        {
            _uof = uof;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateAuthorRecord request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uof.AuthorRepository.CreateAuthor(request);

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

            return Ok(author);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _uof.AuthorRepository.DeleteAutor(id);

            _uof.Save();

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] UpdateAuthorRecord request)
        {
            var authorToValidate = _mapper.Map<CreateAuthorRecord>(request);
            var result = await _validator.ValidateAsync(authorToValidate);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            await _uof.AuthorRepository.UpdateAuthor(request);

            _uof.Save();

            return Ok();
        }
    }
}

using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateAuthorRecord request)
        {
            await _authorService.CreateAuthor(request);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<CreateAuthorRecord>?>> GetAll()
        {
            var list = await _authorService.GetAllAuthors();

            return Ok(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CreateAuthorRecord>> Get(Guid id)
        {
            var author = await _authorService.GetAuthorById(id);

            return Ok(author);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _authorService.DeleteAutor(id);

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] UpdateAuthorRecord request)
        {
            await _authorService.UpdateAuthor(request);

            return Ok();
        }
    }
}

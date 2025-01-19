using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IDeleteAutorUseCase _deleteAutorUseCase;
        private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

        public AuthorController(ICreateAuthorUseCase createAuthorUseCase,
            IDeleteAutorUseCase deleteAutorUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase)
        {
            _createAuthorUseCase = createAuthorUseCase;
            _deleteAutorUseCase = deleteAutorUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] CreateAuthorRecord request)
        {
            await _createAuthorUseCase.Execute(request);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<CreateAuthorRecord>?>> GetAll()
        {
            var list = await _getAllAuthorsUseCase.Execute();

            return Ok(list);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CreateAuthorRecord>> Get(Guid id)
        {
            var author = await _getAuthorByIdUseCase.Execute(id);

            return Ok(author);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _deleteAutorUseCase.Execute(id);

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update([FromBody] UpdateAuthorRecord request)
        {
            await _updateAuthorUseCase.Execute(request);

            return Ok();
        }
    }
}

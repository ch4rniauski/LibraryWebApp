using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Commands.AuthorCommands;
using Application.Queries.AuthorQueries;
using MediatR;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMediator _mediator;

        public AuthorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateAuthor(CreateAuthorRecord author)
        {
            var command = new CreateAuthorCommand { Author = author };

            await _mediator.Send(command);
        }

        public async Task DeleteAutor(Guid id)
        {
            var command = new DeleteAutorCommand { AuthorId = id };

            await _mediator.Send(command);
        }

        public async Task<List<CreateAuthorRecord>?> GetAllAuthors()
        {
            var query = new GetAllAuthorsQuery();

            var list = await _mediator.Send(query);

            return list;
        }

        public async Task<CreateAuthorRecord> GetAuthorById(Guid id)
        {
            var query = new GetAuthorByIdQuery { AuthorId = id };

            var author = await _mediator.Send(query);

            return author;
        }

        public async Task UpdateAuthor(UpdateAuthorRecord author)
        {
            var command = new UpdateAuthorCommand { Author = author };

            await _mediator.Send(command);
        }
    }
}

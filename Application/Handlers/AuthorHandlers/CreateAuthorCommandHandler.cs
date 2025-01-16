using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Commands.AuthorCommands;
using MediatR;

namespace Application.Handlers.AuthorHandlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
    {
        private readonly ICreateAuthorUseCase _createAuthorUseCase;

        public CreateAuthorCommandHandler(ICreateAuthorUseCase createAuthorUseCase)
        {
            _createAuthorUseCase = createAuthorUseCase;
        }

        public async Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _createAuthorUseCase.Execute(request.Author);
        }
    }
}

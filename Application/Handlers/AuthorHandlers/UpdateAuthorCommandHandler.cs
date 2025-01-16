using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Commands.AuthorCommands;
using MediatR;

namespace Application.Handlers.AuthorHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

        public UpdateAuthorCommandHandler(IUpdateAuthorUseCase updateAuthorUseCase)
        {
            _updateAuthorUseCase = updateAuthorUseCase;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _updateAuthorUseCase.Execute(request.Author);
        }
    }
}

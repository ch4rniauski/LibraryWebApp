using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Commands.AuthorCommands;
using MediatR;

namespace Application.Handlers.AuthorHandlers
{
    public class DeleteAutorCommandHandler : IRequestHandler<DeleteAutorCommand>
    {
        private readonly IDeleteAutorUseCase _deleteAuthorUseCase;

        public DeleteAutorCommandHandler(IDeleteAutorUseCase deleteAuthorUseCase)
        {
            _deleteAuthorUseCase = deleteAuthorUseCase;
        }

        public async Task Handle(DeleteAutorCommand request, CancellationToken cancellationToken)
        {
            await _deleteAuthorUseCase.Execute(request.AuthorId);
        }
    }
}

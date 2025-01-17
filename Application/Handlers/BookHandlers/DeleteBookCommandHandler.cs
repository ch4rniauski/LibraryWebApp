using Application.Abstractions.UseCases.BookUseCases;
using Application.Commands.BookCommands;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IDeleteBookUseCase _deleteBookUseCase;

        public DeleteBookCommandHandler(IDeleteBookUseCase deleteBookUseCase)
        {
            _deleteBookUseCase = deleteBookUseCase;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _deleteBookUseCase.Execute(request.BookId);
        }
    }
}

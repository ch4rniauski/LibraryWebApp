using Application.Abstractions.UseCases.BookUseCases;
using Application.Commands.BookCommands;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IUpdateBookUseCase _updateBookUseCase;

        public UpdateBookCommandHandler(IUpdateBookUseCase updateBookUseCase)
        {
            _updateBookUseCase = updateBookUseCase;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            await _updateBookUseCase.Execute(request.Book, request.BookId);
        }
    }
}

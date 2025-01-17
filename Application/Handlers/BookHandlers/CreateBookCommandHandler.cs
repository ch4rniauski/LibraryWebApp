using Application.Abstractions.UseCases.BookUseCases;
using Application.Commands.BookCommands;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
    {
        private readonly ICreateBookUseCase _createBookUseCase;

        public CreateBookCommandHandler(ICreateBookUseCase createBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
        }

        public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await _createBookUseCase.Execute(request.Book);
        }
    }
}

using Application.Abstractions.UseCases.BookUseCases;
using Application.Commands.BookCommands;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
    {
        private readonly IReturnBookUseCase _returnBookUseCase;

        public ReturnBookCommandHandler(IReturnBookUseCase returnBookUseCase)
        {
            _returnBookUseCase = returnBookUseCase;
        }

        public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            await _returnBookUseCase.Execute(request.BookId);
        }
    }
}

using Application.Abstractions.UseCases.UserUseCases;
using Application.Commands.UserCommands;
using MediatR;

namespace Application.Handlers.UserHandlers
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
    {
        private readonly IBorrowBookUseCase _borrowBookUseCase;

        public BorrowBookCommandHandler(IBorrowBookUseCase borrowBookUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
        }

        public async Task Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            await _borrowBookUseCase.Execute(request.UserId, request.BookId);
        }
    }
}

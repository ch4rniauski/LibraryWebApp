using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Commands.AuthenticationUserCommands;
using MediatR;

namespace Application.Handlers.AuthenticationUserHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IDeleteUserUseCase _deleteUserUseCase;

        public DeleteUserCommandHandler(IDeleteUserUseCase deleteUserUseCase)
        {
            _deleteUserUseCase = deleteUserUseCase;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _deleteUserUseCase.Execute(request.UserId);
        }
    }
}

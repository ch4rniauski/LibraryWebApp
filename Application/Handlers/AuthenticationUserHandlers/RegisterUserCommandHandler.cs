using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Commands.AuthenticationUserCommands;
using MediatR;

namespace Application.Handlers.AuthenticationUserHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;

        public RegisterUserCommandHandler(IRegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _registerUserUseCase.Execute(request.User);
        }
    }
}

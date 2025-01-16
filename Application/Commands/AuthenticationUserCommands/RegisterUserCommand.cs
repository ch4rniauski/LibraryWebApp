using Application.Abstractions.Requests;
using MediatR;

namespace Application.Commands.AuthenticationUserCommands
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterUserRecord User { get; set; } = null!;
    }
}

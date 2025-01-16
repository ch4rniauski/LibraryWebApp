using MediatR;

namespace Application.Commands.AuthenticationUserCommands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

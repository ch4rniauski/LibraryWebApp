using MediatR;

namespace Application.Commands.AuthorCommands
{
    public class DeleteAutorCommand : IRequest
    {
        public Guid AuthorId { get; set; }
    }
}

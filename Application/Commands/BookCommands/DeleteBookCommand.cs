using MediatR;

namespace Application.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest
    {
        public Guid BookId { get; set; }
    }
}

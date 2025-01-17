using MediatR;

namespace Application.Commands.BookCommands
{
    public class ReturnBookCommand : IRequest
    {
        public Guid BookId { get; set; }
    }
}

using MediatR;

namespace Application.Commands.UserCommands
{
    public class BorrowBookCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}

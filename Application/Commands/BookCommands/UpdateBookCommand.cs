using Application.Abstractions.Requests;
using MediatR;

namespace Application.Commands.BookCommands
{
    public class UpdateBookCommand : IRequest
    {
        public CreateBookRecord Book { get; set; } = null!;
        public Guid BookId { get; set; }
    }
}

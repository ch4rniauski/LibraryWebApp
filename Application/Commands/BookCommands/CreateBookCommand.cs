using Application.Abstractions.Requests;
using MediatR;

namespace Application.Commands.BookCommands
{
    public class CreateBookCommand : IRequest
    {
        public CreateBookRecord Book { get; set; } = null!;
    }
}

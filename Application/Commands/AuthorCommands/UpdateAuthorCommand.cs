using Application.Abstractions.Requests;
using MediatR;

namespace Application.Commands.AuthorCommands
{
    public class UpdateAuthorCommand : IRequest
    {
        public UpdateAuthorRecord Author { get; set; } = null!;
    }
}

using Application.Abstractions.Requests;
using MediatR;

namespace Application.Commands.AuthorCommands
{
    public class CreateAuthorCommand : IRequest
    {
        public CreateAuthorRecord Author { get; set; } = null!;
    }
}

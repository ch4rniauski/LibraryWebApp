using Application.Abstractions.Requests;
using MediatR;

namespace Application.Queries.AuthorQueries
{
    public class GetAuthorByIdQuery : IRequest<CreateAuthorRecord>
    {
        public Guid AuthorId { get; set; }
    }
}

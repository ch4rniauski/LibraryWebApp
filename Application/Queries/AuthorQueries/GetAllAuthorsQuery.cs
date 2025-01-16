using Application.Abstractions.Requests;
using MediatR;

namespace Application.Queries.AuthorQueries
{
    public class GetAllAuthorsQuery : IRequest<List<CreateAuthorRecord>?>
    {
    }
}

using Application.Abstractions.Records;
using MediatR;

namespace Application.Queries.BookQueries
{
    public class GetBooksByUserIdQuery : IRequest<List<GetBookRecord>?>
    {
        public Guid UserId { get; set; }
    }
}

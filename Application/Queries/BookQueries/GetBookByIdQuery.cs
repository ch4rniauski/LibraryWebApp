using Application.Abstractions.Records;
using MediatR;

namespace Application.Queries.BookQueries
{
    public class GetBookByIdQuery : IRequest<GetBookRecord>
    {
        public Guid BookId { get; set; }
    }
}

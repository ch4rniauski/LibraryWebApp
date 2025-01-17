using Application.Abstractions.Records;
using MediatR;

namespace Application.Queries.BookQueries
{
    public class GetBookByISBNQuery : IRequest<GetBookRecord>
    {
        public string BookISBN { get; set; } = string.Empty;
    }
}

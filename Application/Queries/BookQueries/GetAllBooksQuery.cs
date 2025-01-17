using Application.Abstractions.Records;
using MediatR;

namespace Application.Queries.BookQueries
{
    public class GetAllBooksQuery : IRequest<List<GetBookRecord>?>
    {
    }
}

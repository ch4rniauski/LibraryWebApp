using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using MediatR;

namespace Application.Queries.BookQueries
{
    public class GetBooksWithParamsQuery : IRequest<List<GetBookResponse>?>
    {
        
        public GetBookRequest Request { get; set; } = null!;
    }
}

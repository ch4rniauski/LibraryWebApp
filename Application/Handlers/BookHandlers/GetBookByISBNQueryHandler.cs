using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class GetBookByISBNQueryHandler : IRequestHandler<GetBookByISBNQuery, GetBookRecord>
    {
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;

        public GetBookByISBNQueryHandler(IGetBookByISBNUseCase getBookByISBNUseCase)
        {
            _getBookByISBNUseCase = getBookByISBNUseCase;
        }

        public async Task<GetBookRecord> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
        {
            var response = await _getBookByISBNUseCase.Execute(request.BookISBN);

            return response;
        }
    }
}

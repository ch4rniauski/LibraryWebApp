using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class GetBooksWithParamsQueryHandler : IRequestHandler<GetBooksWithParamsQuery, List<GetBookResponse>?>
    {
        private readonly IGetBooksWithParamsUseCase _getBooksWithParamsUseCase;

        public GetBooksWithParamsQueryHandler(IGetBooksWithParamsUseCase getBooksWithParamsUseCase)
        {
            _getBooksWithParamsUseCase = getBooksWithParamsUseCase;
        }

        public async Task<List<GetBookResponse>?> Handle(GetBooksWithParamsQuery request, CancellationToken cancellationToken)
        {
            var response = await _getBooksWithParamsUseCase.Execute(request.Request);

            return response;
        }
    }
}

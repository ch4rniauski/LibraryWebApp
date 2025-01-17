using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class GetBooksByUserIdQueryHandler : IRequestHandler<GetBooksByUserIdQuery, List<GetBookRecord>?>
    {
        private readonly IGetBooksByUserIdUseCase _getBookByUserIdUseCase;

        public GetBooksByUserIdQueryHandler(IGetBooksByUserIdUseCase getBookByUserIdUseCase)
        {
            _getBookByUserIdUseCase = getBookByUserIdUseCase;
        }

        public async Task<List<GetBookRecord>?> Handle(GetBooksByUserIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _getBookByUserIdUseCase.Execute(request.UserId);

            return response;
        }
    }
}

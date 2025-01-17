using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetBookRecord>
    {
        private readonly IGetBookByIdUseCase _getBookByIDUseCase;

        public GetBookByIdQueryHandler(IGetBookByIdUseCase getBookByIDUseCase)
        {
            _getBookByIDUseCase = getBookByIDUseCase;
        }

        public async Task<GetBookRecord> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _getBookByIDUseCase.Execute(request.BookId);

            return response;
        }
    }
}

using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Queries.BookQueries;
using MediatR;

namespace Application.Handlers.BookHandlers
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<GetBookRecord>?>
    {
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;

        public GetAllBooksQueryHandler(IGetAllBooksUseCase getAllBooksUseCase)
        {
            _getAllBooksUseCase = getAllBooksUseCase;
        }

        public async Task<List<GetBookRecord>?> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _getAllBooksUseCase.Execute();

            return result;
        }
    }
}

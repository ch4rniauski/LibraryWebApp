using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Queries.AuthorQueries;
using MediatR;

namespace Application.Handlers.AuthorHandlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<CreateAuthorRecord>?>
    {
        private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;

        public GetAllAuthorsQueryHandler(IGetAllAuthorsUseCase getAllAuthorsUseCase)
        {
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
        }

        public async Task<List<CreateAuthorRecord>?> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var response = await _getAllAuthorsUseCase.Execute();

            return response;
        }
    }
}

using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Queries.AuthorQueries;
using MediatR;

namespace Application.Handlers.AuthorHandlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, CreateAuthorRecord>
    {
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;

        public GetAuthorByIdQueryHandler(IGetAuthorByIdUseCase getAuthorByIdUseCase)
        {
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
        }

        public async Task<CreateAuthorRecord> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _getAuthorByIdUseCase.Execute(request.AuthorId);

            return response;
        }
    }
}

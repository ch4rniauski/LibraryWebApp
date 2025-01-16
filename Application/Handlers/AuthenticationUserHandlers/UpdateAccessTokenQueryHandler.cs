using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Queries.AuthenticationUserQueries;
using MediatR;

namespace Application.Handlers.AuthenticationUserHandlers
{
    public class UpdateAccessTokenQueryHandler : IRequestHandler<UpdateAccessTokenQuery, string>
    {
        private readonly IUpdateAccessTokenUseCase _updateAccessTokenUseCase;

        public UpdateAccessTokenQueryHandler(IUpdateAccessTokenUseCase updateAccessTokenUseCase)
        {
            _updateAccessTokenUseCase = updateAccessTokenUseCase;
        }

        public Task<string> Handle(UpdateAccessTokenQuery request, CancellationToken cancellationToken)
        {
            var result = _updateAccessTokenUseCase.Execute(request.Id, request.RefreshToken);

            return result;
        }
    }
}

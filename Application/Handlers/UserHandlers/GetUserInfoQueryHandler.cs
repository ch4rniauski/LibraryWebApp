using Application.Abstractions.Records;
using Application.Abstractions.UseCases.UserUseCases;
using Application.Queries.UserQueries;
using MediatR;

namespace Application.Handlers.UserHandlers
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoResponse>
    {
        private readonly IGetUserInfoUseCase _getUserInfoUseCase;

        public GetUserInfoQueryHandler(IGetUserInfoUseCase getUserInfoUseCase)
        {
            _getUserInfoUseCase = getUserInfoUseCase;
        }

        public Task<UserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var response = _getUserInfoUseCase.Execute(request.UserId);

            return response;
        }
    }
}

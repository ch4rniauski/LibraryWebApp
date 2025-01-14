using Application.Abstractions.Records;

namespace Application.Abstractions.UseCases.UserUseCases
{
    public interface IGetUserInfoUseCase
    {
        Task<UserInfoResponse> Execute(Guid id);
    }
}

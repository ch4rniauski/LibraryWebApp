using Application.Abstractions.Records;
using Application.Abstractions.Services;
using Application.Abstractions.UseCases.UserUseCases;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBorrowBookUseCase _borrowBookUseCase;
        private readonly IGetUserInfoUseCase _getUserInfoUseCase;

        public UserService(IBorrowBookUseCase borrowBookUseCase,
            IGetUserInfoUseCase getUserInfoUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _getUserInfoUseCase = getUserInfoUseCase;
        }

        public async Task BorrowBook(Guid userId, Guid bookId)
        {
            await _borrowBookUseCase.Execute(userId, bookId);
        }

        public async Task<UserInfoResponse> GetUserInfo(Guid id)
        {
            var user = await _getUserInfoUseCase.Execute(id);

            return user;
        }
    }
}

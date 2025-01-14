using Application.Abstractions.Records;

namespace Application.Abstractions.Services;

public interface IUserService
{
    Task<UserInfoResponse> GetUserInfo(Guid id);
    Task BorrowBook(Guid userId, Guid bookId);
}

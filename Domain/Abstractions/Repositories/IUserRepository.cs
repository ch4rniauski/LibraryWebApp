using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserInfoResponse?> GetUserInfo(Guid id);
        Task<bool> BorrowBook(Guid userId, Guid bookId);
    }
}

using Domain.Abstractions.Records;
using Domain.Entities;

namespace Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserInfoResponse> GetUserInfo(Guid id);
        Task BorrowBook(Guid userId, Guid bookId);
    }
}

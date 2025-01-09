using Domain.Abstractions.Records;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserInfoResponse> GetUserInfo(Guid id);
        Task BorrowBook(Guid userId, Guid bookId);
        Task<UserEntity?> GetById(Guid id);
        Task<UserEntity?> GetByLogin(string login);
        Task<UserEntity?> GetByEmail(string email);
    }
}

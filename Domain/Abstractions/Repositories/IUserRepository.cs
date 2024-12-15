using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserRecord?> GetUserInfo(Guid id);
    }
}

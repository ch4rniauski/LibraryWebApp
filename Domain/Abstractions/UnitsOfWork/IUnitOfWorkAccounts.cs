using Domain.Abstractions.Repositories;

namespace Domain.Abstractions.UnitsOfWork
{
    public interface IUnitOfWorkAccounts
    {
        IAuthenticationRepository AuthenticationRepository { get; }

        void Save();
    }
}

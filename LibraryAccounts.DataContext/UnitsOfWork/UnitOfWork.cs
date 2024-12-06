using Accounts.DataContext;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;

namespace LibraryAccounts.DataContext.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWorkAccounts
    {
        public IAuthenticationRepository AuthenticationRepository {  get; }
        private readonly AccountsContext _db;

        public UnitOfWork(IAuthenticationRepository authenticationRepository, AccountsContext db)
        {
            AuthenticationRepository = authenticationRepository;
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;

namespace Library.DataContext.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthorRepository AuthorRepository {  get; }
        public IBookRepository BookRepository { get; }
        public IAuthenticationRepository AuthenticationRepository { get; }
        public IUserRepository UserRepository { get; }
        private readonly LibraryContext _db;

        public UnitOfWork(LibraryContext db,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository ,
            IAuthenticationRepository authenticationRepository,
            IUserRepository userRepository)
        {
            _db = db;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            AuthenticationRepository = authenticationRepository;
            UserRepository = userRepository;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;

namespace Library.DataContext.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthorRepository AuthorRepository {  get; }
        public IBookRepository BookRepository { get; }
        private readonly LibraryContext _db;

        public UnitOfWork(LibraryContext db, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _db = db;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
        }

        public async void Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

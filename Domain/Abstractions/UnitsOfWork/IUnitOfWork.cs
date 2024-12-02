using Domain.Abstractions.Repositories;

namespace Domain.Abstractions.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }

        void Save();
    }
}

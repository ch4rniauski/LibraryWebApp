using Domain.Abstractions.Repositories;

namespace Domain.Abstractions.UnitsOfWork
{
    public interface IUnitOfWorkLibrary
    {
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }

        void Save();
    }
}

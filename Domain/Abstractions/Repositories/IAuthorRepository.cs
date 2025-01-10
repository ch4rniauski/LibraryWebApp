using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository : IGenericRepository<AuthorEntity>
    {
        Task<AuthorEntity?> GetByFirstName(string firstName);
        Task<AuthorEntity?> GetBySecondName(string secondName);
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository
    {
        Task<EntityEntry<AuthorEntity>?> CreateAuthor(AuthorEntity author);
        Task<AuthorEntity?> GetById(Guid id);
        Task<AuthorEntity?> GetByFirstName(string firstName);
        Task<AuthorEntity?> GetBySecondName(string secondName);
        Task<List<AuthorEntity>?> GetAllAuthors();
        EntityEntry<AuthorEntity>? DeleteAutor(AuthorEntity author);
    }
}

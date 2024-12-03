using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository
    {
        Task<bool> CreateAuthor(AuthorRecord author);
        Task<AuthorRecord?> GetAuthor(Guid id);
        List<AuthorRecord> GetAllAuthors();
        Task<bool> UpdateAuthor(AuthorRecord author);
        Task<bool> DeleteAutor(Guid id);
    }
}

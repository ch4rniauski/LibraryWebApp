using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository
    {
        Task<bool> CreateAuthor(CreateAuthorRecord author);
        Task<CreateAuthorRecord?> GetAuthor(Guid id);
        List<CreateAuthorRecord> GetAllAuthors();
        Task<bool> UpdateAuthor(UpdateAuthorRecord author);
        Task<bool> DeleteAutor(Guid id);
    }
}

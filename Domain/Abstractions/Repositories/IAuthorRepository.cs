using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository
    {
        Task CreateAuthor(CreateAuthorRecord author);
        Task<CreateAuthorRecord> GetAuthor(Guid id);
        List<CreateAuthorRecord> GetAllAuthors();
        Task UpdateAuthor(UpdateAuthorRecord author);
        Task DeleteAutor(Guid id);
    }
}

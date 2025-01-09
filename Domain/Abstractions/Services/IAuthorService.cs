using Domain.Abstractions.Records;

namespace Domain.Abstractions.Services
{
    public interface IAuthorService
    {
        Task CreateAuthor(CreateAuthorRecord author);
        Task<CreateAuthorRecord> GetAuthorById(Guid id);
        Task<List<CreateAuthorRecord>?> GetAllAuthors();
        Task UpdateAuthor(UpdateAuthorRecord author);
        Task DeleteAutor(Guid id);
    }
}

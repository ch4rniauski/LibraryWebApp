using Application.Abstractions.Records;
using Application.Abstractions.Requests;

namespace Application.Abstractions.Services
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

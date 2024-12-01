using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Abstractions.Repositories
{
    public interface IAuthorRepository
    {
        Task<ActionResult> CreateAuthor(AuthorRecord author);
        Task<ActionResult<AuthorRecord>> GetAuthor(Guid id);
        Task<ActionResult<List<AuthorRecord>>> GetAllAuthors();
        Task<ActionResult> UpdateAuthor(AuthorRecord author);
        Task<ActionResult> DeleteAutor(AuthorRecord author);
    }
}

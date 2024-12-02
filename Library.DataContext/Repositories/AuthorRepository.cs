using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.DataContext.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _db;

        public AuthorRepository(LibraryContext db)
        {
            _db = db;
        }

        public Task<ActionResult> CreateAuthor(AuthorRecord author)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteAutor(AuthorRecord author)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<AuthorRecord>>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<AuthorRecord>> GetAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateAuthor(AuthorRecord author)
        {
            throw new NotImplementedException();
        }
    }
}

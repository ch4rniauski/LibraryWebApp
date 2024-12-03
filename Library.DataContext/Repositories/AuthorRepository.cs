using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _db;

        public AuthorRepository(LibraryContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAuthor(AuthorRecord author)
        {
            AuthorEntity newAuthor = author.Adapt<AuthorEntity>();

            var createdAuthor = await _db.Auhtors.AddAsync(newAuthor);

            return (createdAuthor is not null);
        }

        public async Task<bool> DeleteAutor(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return false;

            _db.Auhtors.Remove(author);

            return true;
        }

        public List<AuthorRecord>? GetAllAuthors()
        {
            var authors = _db.Auhtors;

            if (authors is null)
                return null;

            return _db.Auhtors.Adapt<List<AuthorRecord>>();
        }

        public async Task<AuthorRecord?> GetAuthor(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return null;

            return author.Adapt<AuthorRecord>();
        }

        public async Task<bool> UpdateAuthor(AuthorRecord author)
        {
            var authorToUpdate = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == author.Id);

            if (authorToUpdate is null)
                return false;

            authorToUpdate = author.Adapt<AuthorEntity>();

            return true;
        }
    }
}

using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.DataContext.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _db;

        public AuthorRepository(LibraryContext db)
        {
            _db = db;
        }

        public async Task<EntityEntry<AuthorEntity>?> CreateAuthor(AuthorEntity author)
        {
            var createdAuthor = await _db.Auhtors.AddAsync(author);

            return createdAuthor;
        }

        public EntityEntry<AuthorEntity>? DeleteAutor(AuthorEntity author)
        {
            var isDeleted = _db.Auhtors.Remove(author);

            return isDeleted;
        }

        public async Task<List<AuthorEntity>?> GetAllAuthors()
        {
            var authors = await _db.Auhtors.ToListAsync();

            return authors;
        }

        public async Task<AuthorEntity?> GetById(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }
    }
}

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

        public async Task<bool> CreateAuthor(CreateAuthorRecord author)
        {
            var newAuthor = author.Adapt<AuthorEntity>();
            newAuthor.Id = Guid.NewGuid();

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

        public List<CreateAuthorRecord> GetAllAuthors()
        {
            return _db.Auhtors.Adapt<List<CreateAuthorRecord>>();
        }

        public async Task<CreateAuthorRecord?> GetAuthor(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                return null;

            return author.Adapt<CreateAuthorRecord>();
        }

        public async Task<bool> UpdateAuthor(UpdateAuthorRecord author)
        {
            var authorToUpdate = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == author.Id);

            if (authorToUpdate is null)
                return false;

            authorToUpdate.Id = author.Id;
            authorToUpdate.BirthDate = author.BirthDate;
            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.SecondName = author.SecondName;
            authorToUpdate.Country = author.Country;

            return true;
        }
    }
}

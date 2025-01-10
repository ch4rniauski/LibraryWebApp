using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class AuthorRepository : GenericRepository<AuthorEntity>, IAuthorRepository
    {
        private readonly LibraryContext _db;

        public AuthorRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AuthorEntity?> GetByFirstName(string firstName)
        {
            var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == firstName.ToLower());

            return authorByFirstName;
        }

        public async Task<AuthorEntity?> GetBySecondName(string secondName)
        {
            var authorByFirstName = await _db.Auhtors.FirstOrDefaultAsync(a => a.FirstName.ToLower() == secondName.ToLower());

            return authorByFirstName;
        }
    }
}

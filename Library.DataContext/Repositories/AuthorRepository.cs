using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _db;
        private readonly IMapper _mapper;

        public AuthorRepository(LibraryContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task CreateAuthor(CreateAuthorRecord author)
        {
            var newAuthor = _mapper.Map<AuthorEntity>(author);
            newAuthor.Id = Guid.NewGuid();

            var createdAuthor = await _db.Auhtors.AddAsync(newAuthor);

            if (createdAuthor is null)
                throw new Exception("Author wasn't created");
        }

        public async Task DeleteAutor(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                throw new Exception("Author with that ID doesn't exist");

            _db.Auhtors.Remove(author);
        }

        public List<CreateAuthorRecord> GetAllAuthors()
        {
            var authors = _db.Auhtors;
            return _mapper.Map<List<CreateAuthorRecord>>(authors);
        }

        public async Task<CreateAuthorRecord> GetAuthor(Guid id)
        {
            var author = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
                throw new Exception("Author with that id doesn't exist");

            return _mapper.Map<CreateAuthorRecord>(author);
        }

        public async Task UpdateAuthor(UpdateAuthorRecord author)
        {
            var authorToUpdate = await _db.Auhtors.FirstOrDefaultAsync(a => a.Id == author.Id);

            if (authorToUpdate is null)
                throw new Exception("Author with that id doesn't exist");

            authorToUpdate.Id = author.Id;
            authorToUpdate.BirthDate = author.BirthDate;
            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.SecondName = author.SecondName;
            authorToUpdate.Country = author.Country;
        }
    }
}

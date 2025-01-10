using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Services;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using Domain.Exceptions.CustomExceptions;
using FluentValidation;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorRecord> _validator;

        public AuthorService(IUnitOfWork uow, IMapper mapper, IValidator<CreateAuthorRecord> validator)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = validator;
        }

        public async Task CreateAuthor(CreateAuthorRecord author)
        {
            var result = await _validator.ValidateAsync(author);

            var messages = result.Errors.Select(e => new { e.ErrorMessage }).ToList();
            var message = string.Join(".", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var newAuthor = _mapper.Map<AuthorEntity>(author);
            newAuthor.Id = Guid.NewGuid();

            var createdAuthor = await _uow.AuthorRepository.Create(newAuthor);

            if (createdAuthor is null)
                throw new CreationFailureException("Author wasn't created");

            await _uow.Save();
        }

        public async Task DeleteAutor(Guid id)
        {
            var author = await _uow.AuthorRepository.GetById(id);

            if (author is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            var isDeleted = _uow.AuthorRepository.Delete(author);

            if (isDeleted is null)
                throw new RemovalFailureException("Author with that ID wasn't deleted");

            await _uow.Save();
        }

        public async Task<List<CreateAuthorRecord>?> GetAllAuthors()
        {
            var list = await _uow.AuthorRepository.GetAll();

            if (list is null)
                return null;

            var listToReturn = _mapper.Map<List<CreateAuthorRecord>>(list);

            return listToReturn;
        }

        public async Task<CreateAuthorRecord> GetAuthorById(Guid id)
        {
            var author = await _uow.AuthorRepository.GetById(id);

            if (author is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            return _mapper.Map<CreateAuthorRecord>(author);
        }

        public async Task UpdateAuthor(UpdateAuthorRecord author)
        {
            var authorToValidate = _mapper.Map<CreateAuthorRecord>(author);
            var result = await _validator.ValidateAsync(authorToValidate);

            var messages = result.Errors.Select(e => new { e.ErrorMessage }).ToList();
            var message = string.Join(".", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var authorToUpdate = await _uow.AuthorRepository.GetById(author.Id);

            if (authorToUpdate is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            authorToUpdate.Id = author.Id;
            authorToUpdate.BirthDate = author.BirthDate;
            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.SecondName = author.SecondName;
            authorToUpdate.Country = author.Country;

            await _uow.Save();
        }
    }
}

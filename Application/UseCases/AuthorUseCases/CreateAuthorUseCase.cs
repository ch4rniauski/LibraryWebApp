using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.AuthorUseCases
{
    public class CreateAuthorUseCase : ICreateAuthorUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorRecord> _validator;

        public CreateAuthorUseCase(IUnitOfWork uow, IMapper mapper, IValidator<CreateAuthorRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Execute(CreateAuthorRecord author)
        {
            var result = await _validator.ValidateAsync(author);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var newAuthor = _mapper.Map<AuthorEntity>(author);
            newAuthor.Id = Guid.NewGuid();

            var isCreated = await _uow.AuthorRepository.Create(newAuthor);

            if (!isCreated)
                throw new CreationFailureException("Author wasn't created");

            await _uow.Save();
        }
    }
}

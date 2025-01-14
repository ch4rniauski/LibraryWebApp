using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;

namespace Application.UseCases.AuthorUseCases
{
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorRecord> _validator;

        public UpdateAuthorUseCase(IUnitOfWork uow, IMapper mapper, IValidator<CreateAuthorRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Execute(UpdateAuthorRecord author)
        {
            var authorToValidate = _mapper.Map<CreateAuthorRecord>(author);
            var result = await _validator.ValidateAsync(authorToValidate);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var authorToUpdate = await _uow.AuthorRepository.GetById(author.Id);

            if (authorToUpdate is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            _mapper.Map(author, authorToUpdate);

            await _uow.Save();
        }
    }
}

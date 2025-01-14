using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCases.BookUseCases
{
    public class CreateBookUseCase : ICreateBookUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookRecord> _validator;

        public CreateBookUseCase(IUnitOfWork uow, IMapper mapper, IValidator<CreateBookRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Execute(CreateBookRecord book)
        {
            var result = await _validator.ValidateAsync(book);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var newBook = _mapper.Map<BookEntity>(book);

            newBook.Id = Guid.NewGuid();
            newBook.AuthorFirstName = null;
            newBook.AuthorSecondName = null;

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _uow.AuthorRepository.GetByFirstName(book.AuthorFirstName);

                if (authorByFirstName is not null)
                {
                    newBook.AuthorId = authorByFirstName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorSecondName is not null && book.AuthorSecondName.ToLower() != authorByFirstName.SecondName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        newBook.AuthorFirstName = authorByFirstName.FirstName;
                        newBook.AuthorSecondName = authorByFirstName.SecondName;
                    }
                }
                if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

                    if (authorBySecondName is not null)
                    {
                        newBook.AuthorId = authorBySecondName.Id;
                        isAuthorChanged = true;

                        if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.SecondName.ToLower())
                            isAuthorChanged = false;
                        else
                        {
                            newBook.AuthorFirstName = authorBySecondName.FirstName;
                            newBook.AuthorSecondName = authorBySecondName.SecondName;
                        }
                    }
                }
            }
            if (isAuthorChanged == false && book.AuthorSecondName is not null)
            {
                var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

                if (authorBySecondName is not null)
                {
                    newBook.AuthorId = authorBySecondName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.FirstName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        newBook.AuthorFirstName = authorBySecondName.FirstName;
                        newBook.AuthorSecondName = authorBySecondName.SecondName;
                    }
                }
            }

            if ((book.AuthorFirstName is not null || book.AuthorSecondName is not null) && !isAuthorChanged)
                throw new NotFoundException("Author with that name doesn't exist");

            var isCreated = await _uow.BookRepository.Create(newBook);

            if (!isCreated)
                throw new CreationFailureException("Author wasn't created");

            await _uow.Save();
        }
    }
}

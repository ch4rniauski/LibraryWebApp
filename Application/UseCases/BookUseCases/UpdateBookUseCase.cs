using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using FluentValidation;

namespace Application.UseCases.BookUseCases
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookRecord> _validator;

        public UpdateBookUseCase(IUnitOfWork uow, IMapper mapper, IValidator<CreateBookRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Execute(CreateBookRecord book, Guid id)
        {
            var result = await _validator.ValidateAsync(book);

            var messages = result.Errors;
            var message = string.Join("", messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var bookToUpdate = await _uow.BookRepository.GetById(id);

            if (bookToUpdate is null)
                throw new NotFoundException("Book with that ID wasn't found");

            if (book.AuthorFirstName is null && book.AuthorSecondName is null)
            {
                bookToUpdate.AuthorId = null;
                bookToUpdate.AuthorFirstName = null;
                bookToUpdate.AuthorSecondName = null;
            }

            _mapper.Map(book, bookToUpdate);

            var isAuthorChanged = false;

            if (book.AuthorFirstName is not null)
            {
                var authorByFirstName = await _uow.AuthorRepository.GetByFirstName(book.AuthorFirstName);

                if (authorByFirstName is not null)
                {
                    bookToUpdate.AuthorId = authorByFirstName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorSecondName is not null && book.AuthorSecondName.ToLower() != authorByFirstName.SecondName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        bookToUpdate.AuthorFirstName = authorByFirstName.FirstName;
                        bookToUpdate.AuthorSecondName = authorByFirstName.SecondName;
                    }
                }
                if (book.AuthorSecondName is not null)
                {
                    var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

                    if (authorBySecondName is not null)
                    {
                        bookToUpdate.AuthorId = authorBySecondName.Id;
                        isAuthorChanged = true;

                        if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.SecondName.ToLower())
                            isAuthorChanged = false;
                        else
                        {
                            bookToUpdate.AuthorFirstName = authorBySecondName.FirstName;
                            bookToUpdate.AuthorSecondName = authorBySecondName.SecondName;
                        }
                    }
                }
            }
            if (isAuthorChanged == false && book.AuthorSecondName is not null)
            {
                var authorBySecondName = await _uow.AuthorRepository.GetBySecondName(book.AuthorSecondName);

                if (authorBySecondName is not null)
                {
                    bookToUpdate.AuthorId = authorBySecondName.Id;
                    isAuthorChanged = true;

                    if (book.AuthorFirstName is not null && book.AuthorFirstName.ToLower() != authorBySecondName.FirstName.ToLower())
                        isAuthorChanged = false;
                    else
                    {
                        bookToUpdate.AuthorFirstName = authorBySecondName.FirstName;
                        bookToUpdate.AuthorSecondName = authorBySecondName.SecondName;
                    }
                }
            }

            if ((book.AuthorFirstName is not null || book.AuthorSecondName is not null) && !isAuthorChanged)
                throw new NotFoundException("Author with that name doesn't exist");

            await _uow.Save();
        }
    }
}

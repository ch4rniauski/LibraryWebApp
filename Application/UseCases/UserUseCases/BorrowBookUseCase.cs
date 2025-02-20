﻿using Application.Abstractions.UseCases.UserUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.UserUseCases
{
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        private readonly IUnitOfWork _uow;

        public BorrowBookUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Execute(Guid userId, Guid bookId)
        {
            var user = await _uow.UserRepository.GetById(userId);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            var book = await _uow.BookRepository.GetById(bookId);

            if (book is null)
                throw new NotFoundException("Book with that ID doesn't exist");
            if (book.UserId is not null)
                throw new IncorrectDataException("That book is already borrowed");

            book.TakenAt = DateOnly.FromDateTime(DateTime.UtcNow);
            book.DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
            book.UserId = user.Id;
            book.User = user;

            user.Books.Add(book);

            await _uow.Save();
        }
    }
}

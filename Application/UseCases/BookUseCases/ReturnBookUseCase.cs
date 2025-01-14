using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IUnitOfWork _uow;

        public ReturnBookUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Execute(Guid id)
        {
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID doesn't exist");

            book.UserId = null;
            book.User = null;
            book.TakenAt = null;
            book.DueDate = null;

            await _uow.Save();
        }
    }
}

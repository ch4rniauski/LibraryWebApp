using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _uow;

        public DeleteBookUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Execute(Guid id)
        {
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID wasn't found");

            var isDeleted = _uow.BookRepository.Delete(book);

            if (!isDeleted)
                throw new RemovalFailureException("Author with that ID wasn't deleted");

            await _uow.Save();
        }
    }
}

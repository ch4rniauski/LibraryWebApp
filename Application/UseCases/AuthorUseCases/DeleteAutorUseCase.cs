using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.AuthorUseCases
{
    public class DeleteAutorUseCase : IDeleteAutorUseCase
    {
        private readonly IUnitOfWork _uow;

        public DeleteAutorUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Execute(Guid id)
        {
            var author = await _uow.AuthorRepository.GetById(id);

            if (author is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            var isDeleted = _uow.AuthorRepository.Delete(author);

            if (isDeleted is null)
                throw new RemovalFailureException("Author with that ID wasn't deleted");

            await _uow.Save();
        }
    }
}

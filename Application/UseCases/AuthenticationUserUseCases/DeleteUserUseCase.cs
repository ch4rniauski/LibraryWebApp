using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.AuthenticationUserUseCases
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUnitOfWork _uow;

        public DeleteUserUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Execute(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new NotFoundException("User with that ID wasn't found");

            var isDeleted = _uow.AuthenticationRepository.Delete(user);

            if (isDeleted is null)
                throw new RemovalFailureException("User with that ID wasn't deleted");

            await _uow.Save();
        }
    }
}

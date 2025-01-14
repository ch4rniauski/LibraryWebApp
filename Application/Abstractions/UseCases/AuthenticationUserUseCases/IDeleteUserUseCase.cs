namespace Application.Abstractions.UseCases.AuthenticationUserUseCases
{
    public interface IDeleteUserUseCase
    {
        Task Execute(Guid id);
    }
}

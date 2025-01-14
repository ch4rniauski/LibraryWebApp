namespace Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IDeleteAutorUseCase
    {
        Task Execute(Guid id);
    }
}

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IDeleteBookUseCase
    {
        Task Execute(Guid id);
    }
}

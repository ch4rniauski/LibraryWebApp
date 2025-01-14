namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IReturnBookUseCase
    {
        Task Execute(Guid id);
    }
}

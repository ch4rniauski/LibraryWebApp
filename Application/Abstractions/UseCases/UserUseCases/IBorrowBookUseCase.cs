namespace Application.Abstractions.UseCases.UserUseCases
{
    public interface IBorrowBookUseCase
    {
        Task Execute(Guid userId, Guid bookId);
    }
}

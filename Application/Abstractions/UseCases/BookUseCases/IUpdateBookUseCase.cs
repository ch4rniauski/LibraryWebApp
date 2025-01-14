using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IUpdateBookUseCase
    {
        Task Execute(CreateBookRecord book, Guid id);
    }
}

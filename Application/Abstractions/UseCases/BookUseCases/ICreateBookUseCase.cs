using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface ICreateBookUseCase
    {
        Task Execute(CreateBookRecord book);
    }
}

using Application.Abstractions.Records;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetBookByISBNUseCase
    {
        Task<GetBookRecord> Execute(string ISBN);
    }
}

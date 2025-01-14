using Application.Abstractions.Records;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetAllBooksUseCase
    {
        Task<List<GetBookRecord>?> Execute();
    }
}

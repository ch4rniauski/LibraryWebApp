using Application.Abstractions.Records;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetBooksByUserIdUseCase
    {
        Task<List<GetBookRecord>?> Execute(Guid id);
    }
}

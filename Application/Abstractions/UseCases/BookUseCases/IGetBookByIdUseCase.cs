using Application.Abstractions.Records;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetBookByIdUseCase
    {
        Task<GetBookRecord> Execute(Guid id);
    }
}

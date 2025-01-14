using Application.Abstractions.Records;
using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.BookUseCases
{
    public interface IGetBooksWithParamsUseCase
    {
        Task<List<GetBookResponse>?> Execute(GetBookRequest request);
    }
}

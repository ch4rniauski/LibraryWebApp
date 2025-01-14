using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetAllAuthorsUseCase
    {
        Task<List<CreateAuthorRecord>?> Execute();
    }
}

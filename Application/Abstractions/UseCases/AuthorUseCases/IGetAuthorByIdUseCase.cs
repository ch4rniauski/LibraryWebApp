using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IGetAuthorByIdUseCase
    {
        Task<CreateAuthorRecord> Execute(Guid id);
    }
}

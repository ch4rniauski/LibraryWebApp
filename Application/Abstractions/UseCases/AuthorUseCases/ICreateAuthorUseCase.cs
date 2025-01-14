using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthorUseCases
{
    public interface ICreateAuthorUseCase
    {
        Task Execute(CreateAuthorRecord author);
    }
}

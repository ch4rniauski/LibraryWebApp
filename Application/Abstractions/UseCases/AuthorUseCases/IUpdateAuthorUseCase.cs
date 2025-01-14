using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthorUseCases
{
    public interface IUpdateAuthorUseCase
    {
        Task Execute(UpdateAuthorRecord author);
    }
}

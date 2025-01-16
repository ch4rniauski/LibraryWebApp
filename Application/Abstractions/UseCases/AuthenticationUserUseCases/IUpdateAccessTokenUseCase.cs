namespace Application.Abstractions.UseCases.AuthenticationUserUseCases
{
    public interface IUpdateAccessTokenUseCase
    {
        Task<string> Execute(Guid id, string? refreshToken);
    }
}

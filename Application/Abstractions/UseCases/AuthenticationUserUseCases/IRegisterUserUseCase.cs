using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthenticationUserUseCases
{
    public interface IRegisterUserUseCase
    {
        Task Execute(RegisterUserRecord user);
    }
}

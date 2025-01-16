using Application.Abstractions.Records;
using Application.Abstractions.Requests;

namespace Application.Abstractions.UseCases.AuthenticationUserUseCases
{
    public interface ILogInUserUseCase
    {
        Task<LogInResponseRecord> Execute(LogInRequest user);
    }
}

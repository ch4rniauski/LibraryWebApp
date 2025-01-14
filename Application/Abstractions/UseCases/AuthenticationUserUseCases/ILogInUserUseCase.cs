using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.UseCases.AuthenticationUserUseCases
{
    public interface ILogInUserUseCase
    {
        Task<LogInResponseRecord> Execute(LogInRequest user, HttpContext context);
    }
}

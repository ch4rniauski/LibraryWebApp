using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly ILogInUserUseCase _logInUserUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IUpdateAccessTokenUseCase _updateAccessTokenUseCase;

        public AuthenticationUserService(IDeleteUserUseCase deleteUserUseCase,
            ILogInUserUseCase logInUserUseCase,
            IRegisterUserUseCase registerUserUseCase,
            IUpdateAccessTokenUseCase updateAccessTokenUseCase)
        {
            _deleteUserUseCase = deleteUserUseCase;
            _logInUserUseCase = logInUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _updateAccessTokenUseCase = updateAccessTokenUseCase;
        }

        public async Task DeleteUser(Guid id)
        {
            await _deleteUserUseCase.Execute(id);
        }

        public async Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context)
        {
            var response = await _logInUserUseCase.Execute(user, context);
            
            return response;
        }

        public async Task RegisterUser(RegisterUserRecord user)
        {            
            await _registerUserUseCase.Execute(user);
        }

        public async Task<string> UpdateAccessToken(Guid id, HttpContext context)
        {
            var accessToken = await _updateAccessTokenUseCase.Execute(id, context);

            return accessToken;
        }
    }
}

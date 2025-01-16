using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.JWT;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.AuthenticationUserUseCases
{
    public class LogInUserUseCase : ILogInUserUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenProvider _tokenProvider;

        public LogInUserUseCase(IUnitOfWork uow, ITokenProvider tokenProvider)
        {
            _uow = uow;
            _tokenProvider = tokenProvider;
        }

        public async Task<LogInResponseRecord> Execute(LogInRequest user)
        {
            bool isAdmin = false;
            var userEntity = await _uow.UserRepository.GetByLogin(user.Login);

            if (userEntity is null)
                throw new NotFoundException("User with that logn wasn't found");

            if (userEntity.Email != user.Email)
                throw new NotFoundException("User with that Email wasn't found");

            var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new IncorrectDataException("Incorrect password");

            var accessToken = _tokenProvider.GenerateAccessToken(userEntity);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            userEntity.RefreshToken = refreshToken;
            userEntity.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(3);

            if (userEntity.IsAdmin)
                isAdmin = true;

            await _uow.Save();

            return new LogInResponseRecord(userEntity.Id, accessToken, refreshToken, isAdmin);
        }
    }
}

using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Exceptions.CustomExceptions;
using Domain.Abstractions.JWT;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.AuthenticationUserUseCases
{
    public class UpdateAccessTokenUseCase : IUpdateAccessTokenUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenProvider _tokenProvider;

        public UpdateAccessTokenUseCase(IUnitOfWork uow, ITokenProvider tokenProvider)
        {
            _uow = uow;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Execute(Guid id, string? refreshToken)
        {
            if (refreshToken is null)
                throw new NotFoundException("Refresh token doesn't exist");

            var user = await _uow.UserRepository.GetById(id);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new IncorrectDataException("Either user with that ID doesn't exist or refresh token has expired");

            var accessToken = _tokenProvider.GenerateAccessToken(user);

            return accessToken;
        }
    }
}

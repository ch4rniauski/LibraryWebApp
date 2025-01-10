using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.Services;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using Domain.JWT;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly IUnitOfWork _uow; 
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterUserRecord> _validator;

        public AuthenticationUserService(IUnitOfWork uow, TokenProvider tokenProvider, IValidator<RegisterUserRecord> validator, IMapper mapper)
        {
            _uow = uow;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new Exception("User with that ID wasn't found");

            var isDeleted = _uow.AuthenticationRepository.Delete(user);

            if (isDeleted is null)
                throw new Exception("User with that ID wasn't deleted");

            await _uow.Save();
        }

        public async Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context)
        {
            var userEntity = await _uow.UserRepository.GetByLogin(user.Login);

            if (userEntity is null)
                throw new Exception("User with that logn wasn't found");

            if (userEntity.Email != user.Email)
                throw new Exception("User with that Email wasn't found");

            var result = new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Incorrect password");

            var accessToken = _tokenProvider.GenerateAccessToken(userEntity);
            var refreshToken = _tokenProvider.GenerateRefreshToken();

            userEntity.RefreshToken = refreshToken;
            userEntity.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(3);

            context.Response.Cookies.Append("accessToken", accessToken);
            context.Response.Cookies.Append("refreshToken", refreshToken);

            if (userEntity.IsAdmin)
                context.Response.Cookies.Append("admin", "true");

            await _uow.Save();

            return new LogInResponseRecord(userEntity.Id, accessToken, refreshToken);
        }

        public async Task RegisterUser(RegisterUserRecord user)
        {
            var result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
                throw new Exception(); //return BadRequest(result.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));

            var isUserExist = await _uow.UserRepository.GetByLogin(user.Login);

            if (isUserExist is not null)
                throw new Exception("User with that login already exists");

            isUserExist = await _uow.UserRepository.GetByEmail(user.Email);

            if (isUserExist is not null)
                throw new Exception("User with that email already exists");

            var userEntity = _mapper.Map<UserEntity>(user);

            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, user.Password);

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = passwordHash;

            if (user.IsAdmin == "true")
                userEntity.IsAdmin = true;
            else
                userEntity.IsAdmin = false;

            var registeredUser = await _uow.AuthenticationRepository.Create(userEntity);

            if (registeredUser is null)
                throw new Exception("User wasn't registered");

            await _uow.Save();
        }

        public async Task<string> UpdateAccessToken(Guid id, HttpContext context)
        {
            context.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);

            if (refreshToken is null)
                throw new Exception("Refresh token doesn't exist");

            var user = await _uow.UserRepository.GetById(id);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new Exception("Either user with that ID doesn't exist or refresh token has expired");

            var accessToken = _tokenProvider.GenerateAccessToken(user);

            context.Response.Cookies.Append("accessToken", accessToken);

            return accessToken;
        }
    }
}

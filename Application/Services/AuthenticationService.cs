using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Exceptions.CustomExceptions;
using Application.JWT;
using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
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
                throw new NotFoundException("User with that ID wasn't found");

            var isDeleted = _uow.AuthenticationRepository.Delete(user);

            if (isDeleted is null)
                throw new RemovalFailureException("User with that ID wasn't deleted");

            await _uow.Save();
        }

        public async Task<LogInResponseRecord> LogInUser(LogInRequest user, HttpContext context)
        {
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

            var messages = result.Errors;
            var message = string.Join("",messages);

            if (!result.IsValid)
                throw new IncorrectDataException(message);

            var isUserExist = await _uow.UserRepository.GetByLogin(user.Login);

            if (isUserExist is not null)
                throw new AlreadyExistsException("User with that login already exists");

            isUserExist = await _uow.UserRepository.GetByEmail(user.Email);

            if (isUserExist is not null)
                throw new AlreadyExistsException("User with that email already exists");

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
                throw new CreationFailureException("User wasn't registered");

            await _uow.Save();
        }

        public async Task<string> UpdateAccessToken(Guid id, HttpContext context)
        {
            context.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);

            if (refreshToken is null)
                throw new NotFoundException("Refresh token doesn't exist");

            var user = await _uow.UserRepository.GetById(id);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                throw new IncorrectDataException("Either user with that ID doesn't exist or refresh token has expired");

            var accessToken = _tokenProvider.GenerateAccessToken(user);

            context.Response.Cookies.Append("accessToken", accessToken);

            return accessToken;
        }
    }
}

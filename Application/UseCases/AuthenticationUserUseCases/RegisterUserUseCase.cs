using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.AuthenticationUserUseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterUserRecord> _validator;

        public RegisterUserUseCase(IUnitOfWork uow, IMapper mapper, IValidator<RegisterUserRecord> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Execute(RegisterUserRecord user)
        {
            var result = await _validator.ValidateAsync(user);

            var messages = result.Errors;
            var message = string.Join("", messages);

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

            if (!registeredUser)
                throw new CreationFailureException("User wasn't registered");

            await _uow.Save();
        }
    }
}

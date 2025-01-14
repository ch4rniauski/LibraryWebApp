using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Exceptions.CustomExceptions;
using Application.UseCases.AuthenticationUserUseCases;
using AutoMapper;
using Domain.Abstractions.JWT;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace xUnitTests.UseCases
{
    public class AuthenticationUserUseCasesTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ITokenProvider> _tokenProvider = new();
        private readonly Mock<IValidator<RegisterUserRecord>> _validatorMock = new();

        [Fact]
        public async Task DeleteUserUseCase_ThrowsAnExceptionThatUserWithThatIdWasntFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ThrowsAsync(new NotFoundException("User with that ID wasn't found"));

            var deleteUserUseCase = new DeleteUserUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await deleteUserUseCase.Execute(id));
        }

        [Fact]
        public async Task DeleteUserUseCase_Done()
        {
            // Arrange
            var userEntity = new UserEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(userEntity);
            _unitOfWork.Setup(u => u.AuthenticationRepository.Delete(userEntity)).Returns(true);

            var deleteUserUseCase = new DeleteUserUseCase(_unitOfWork.Object);

            // Act
            await deleteUserUseCase.Execute(id);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserUseCase_ThrowsAnExceptionThatUserWasntDeleted()
        {
            // Arrange
            var userEntity = new UserEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(userEntity);
            _unitOfWork.Setup(u => u.AuthenticationRepository.Delete(userEntity)).Returns(false);

            var deleteUserUseCase = new DeleteUserUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<RemovalFailureException>(async () => await deleteUserUseCase.Execute(id));
        }

        [Fact]
        public async Task LoGin_ThrowsAnExceptionThatUserWasntFound()
        {
            // Arrange
            var loginData = new LogInRequest(
                "LoginThatDoesntExist",
                "email_that_doesnt_exist@mail.ru",
                "Incorrect Password");
            var context = new HttpContextAccessor();
            UserEntity? userEntity = null;

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin("LoginThatDoesntExist")).ReturnsAsync(userEntity);

            var logInUserUseCase = new LogInUserUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await logInUserUseCase.Execute(loginData, context.HttpContext!));
        }

        [Fact]
        public async Task LoGin_ThrowsAnExceptionThatPasswordIsIncorrect()
        {
            // Arrange
            var loginData = new LogInRequest(
                "Login",
                "email@mail.ru",
                "Password");
            var context = new HttpContextAccessor();
            var userEntity = new UserEntity
            {
                Email = "email@mail.ru"
            };

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin("Login")).ReturnsAsync(userEntity);

            var logInUserUseCase = new LogInUserUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await logInUserUseCase.Execute(loginData, context.HttpContext!));
        }

        [Fact]
        public async Task LoGin_Done()
        {
            // Arrange
            var loginData = new LogInRequest(
                "Login",
                "email@mail.ru",
                "Password");
            var userEntity = new UserEntity
            {
                Email = "email@mail.ru",
                IsAdmin = false
            };
            var passwordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, loginData.Password);
            userEntity.PasswordHash = passwordHash;

            var httpContextMock = new Mock<HttpContext>();
            var responseMock = new Mock<HttpResponse>();
            var cookiesMock = new Mock<IResponseCookies>();
            responseMock.Setup(r => r.Cookies).Returns(cookiesMock.Object);
            httpContextMock.Setup(c => c.Response).Returns(responseMock.Object);

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin("Login")).ReturnsAsync(userEntity);

            var logInUserUseCase = new LogInUserUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act
            var result = await logInUserUseCase.Execute(loginData, httpContextMock.Object);

            // Assert
            Assert.IsType<LogInResponseRecord>(result);
        }

        [Fact]
        public async Task Register_ThrowsAnExceptionWithValidationErrors()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "Invalid Email",
                "Password",
                "false");
            var validationResult = new Mock<FluentValidation.Results.ValidationFailure>();
            
            _validatorMock.Setup(v => v.ValidateAsync(registerUser, It.IsAny<CancellationToken>())).ThrowsAsync(new IncorrectDataException("Incorrect data"));
            
            var registerUserUseCase = new RegisterUserUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await registerUserUseCase.Execute(registerUser));
        }

        [Fact]
        public async Task Register_ThrowsAnExceptionThatUserWithThatLoginAlreadyExists()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);
            var userEntityByLogin = new UserEntity();

            _validatorMock.Setup(v => v.ValidateAsync(registerUser, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin(registerUser.Login)).ReturnsAsync(userEntityByLogin);

            var registerUserUseCase = new RegisterUserUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await registerUserUseCase.Execute(registerUser));
        }

        [Fact]
        public async Task Register_ThrowsAnExceptionThatUserWithThatEmailAlreadyExists()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);
            UserEntity? userEntityByLogin = null;
            var userEntityByEmail = new UserEntity();

            _validatorMock.Setup(v => v.ValidateAsync(registerUser, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin(registerUser.Login)).ReturnsAsync(userEntityByLogin);
            _unitOfWork.Setup(u => u.UserRepository.GetByEmail(registerUser.Email)).ReturnsAsync(userEntityByEmail);

            var registerUserUseCase = new RegisterUserUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await registerUserUseCase.Execute(registerUser));
        }

        [Fact]
        public async Task Register_ThrowsAnExceptionThatUserWasntCreated()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);
            UserEntity? userEntityByLogin = null;
            UserEntity? userEntityByEmail = null;

            _validatorMock.Setup(v => v.ValidateAsync(registerUser, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin(registerUser.Login)).ReturnsAsync(userEntityByLogin);
            _unitOfWork.Setup(u => u.UserRepository.GetByEmail(registerUser.Email)).ReturnsAsync(userEntityByEmail);
            _unitOfWork.Setup(u => u.AuthenticationRepository.Create(It.IsAny<UserEntity>())).ReturnsAsync(false);

            _mapperMock.Setup(m => m.Map<UserEntity>(It.IsAny<RegisterUserRecord>())).Returns((RegisterUserRecord src) => new UserEntity
            {
                Login = src.Login,
                Email = src.Email
            });

            var registerUserUseCase = new RegisterUserUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<CreationFailureException>(async () => await registerUserUseCase.Execute(registerUser));
        }

        [Fact]
        public async Task Register_Done()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);
            UserEntity? userEntityByLogin = null;
            UserEntity? userEntityByEmail = null;

            _validatorMock.Setup(v => v.ValidateAsync(registerUser, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            _unitOfWork.Setup(u => u.UserRepository.GetByLogin(registerUser.Login)).ReturnsAsync(userEntityByLogin);
            _unitOfWork.Setup(u => u.UserRepository.GetByEmail(registerUser.Email)).ReturnsAsync(userEntityByEmail);
            _unitOfWork.Setup(u => u.AuthenticationRepository.Create(It.IsAny<UserEntity>())).ReturnsAsync(true);

            _mapperMock.Setup(m => m.Map<UserEntity>(It.IsAny<RegisterUserRecord>())).Returns((RegisterUserRecord src) => new UserEntity
            {
                Login = src.Login,
                Email = src.Email
            });

            var registerUserUseCase = new RegisterUserUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act
            await registerUserUseCase.Execute(registerUser);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task UpdateAccessToken_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            var httpContextMock = new Mock<HttpContext>();
            var requesteMock = new Mock<HttpRequest>();
            var cookiesMock = new Mock<IRequestCookieCollection>();

            requesteMock.Setup(r => r.Cookies).Returns(cookiesMock.Object);
            httpContextMock.Setup(c => c.Request).Returns(requesteMock.Object);

            var updateAccessTokenUseCase = new UpdateAccessTokenUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await updateAccessTokenUseCase.Execute(id, httpContextMock.Object));
        }
        
        [Fact]
        public async Task UpdateAccessToken_ThrowsIncorrectDataException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var context = new DefaultHttpContext();
            var refreshToken = "refreshToken";

            UserEntity? user = null;

            context.Request.Cookies = new Mock<IRequestCookieCollection>().Object;
            Mock.Get(context.Request.Cookies).Setup(cookies => cookies.TryGetValue("refreshToken", out refreshToken)).Returns(true);

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(user);

            var updateAccessTokenUseCase = new UpdateAccessTokenUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await updateAccessTokenUseCase.Execute(id, context));
        }

        [Fact]
        public async Task UpdateAccessToken_Done()
        {
            // Arrange
            var id = Guid.NewGuid();
            var context = new DefaultHttpContext();
            var refreshToken = "refreshToken";

            var user = new UserEntity
            {
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(1),
            };

            _tokenProvider.Setup(t => t.GenerateAccessToken(user)).Returns("accessToken");

            context.Request.Cookies = new Mock<IRequestCookieCollection>().Object;
            Mock.Get(context.Request.Cookies).Setup(cookies => cookies.TryGetValue("refreshToken", out refreshToken)).Returns(true);

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(user);

            var updateAccessTokenUseCase = new UpdateAccessTokenUseCase(_unitOfWork.Object, _tokenProvider.Object);

            // Act
            var result = await updateAccessTokenUseCase.Execute(id, context);

            // Assert
            Assert.IsType<string>(result);
        }
    }
}

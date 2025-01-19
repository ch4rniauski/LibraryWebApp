using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllersTests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IDeleteUserUseCase> _deleteUserUseCase = new();
        private readonly Mock<ILogInUserUseCase> _logInUserUseCase = new();
        private readonly Mock<IRegisterUserUseCase> _registerUserUseCase = new();
        private readonly Mock<IUpdateAccessTokenUseCase> _updateAccessTokenUseCase = new();

        [Fact]
        public async Task Register_ThrowsExceptionWithValidationErrors()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "Invalid Email",
                "Password",
                "false");

            _registerUserUseCase.Setup(r => r.Execute(registerUser)).ThrowsAsync(new IncorrectDataException("Incorrect data"));
            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await controller.Register(registerUser));
        }

        [Fact]
        public async Task Register_ReturnsOk()
        {
            // Arrange
            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            _registerUserUseCase.Setup(r => r.Execute(registerUser)).Returns(Task.CompletedTask);


            // Act
            var result = await controller.Register(registerUser);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Register_ThrowsAnException()
        {
            // Arrange
            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            _registerUserUseCase.Setup(r => r.Execute(registerUser)).ThrowsAsync(new AlreadyExistsException("User with that login already exists"));

            // Act

            // Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await controller.Register(registerUser));
        }

        [Fact]
        public async Task LoGin_ThrowsAnException()
        {
            // Arrange
            var loginData = new LogInRequest(
                "LoginThatDoesntExist",
                "email_that_doesnt_exist@mail.ru",
                "Incorrect Password");
            _logInUserUseCase.Setup(l => l.Execute(loginData)).ThrowsAsync(new NotFoundException("User with that Email wasn't found"));
            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.LogIn(loginData));
        }

        [Fact]
        public async Task DeleteUser_ThrowsAnExceptionThatUserWithThatIdWasntFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _deleteUserUseCase.Setup(d => d.Execute(id)).ThrowsAsync(new NotFoundException("User with that ID wasn't found"));

            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.DeleteUser(id));
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _deleteUserUseCase.Setup(d => d.Execute(id)).Returns(Task.CompletedTask);

            var controller = new AuthenticationController(_deleteUserUseCase.Object, _logInUserUseCase.Object, _registerUserUseCase.Object, _updateAccessTokenUseCase.Object);

            // Act
            var result = await controller.DeleteUser(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}

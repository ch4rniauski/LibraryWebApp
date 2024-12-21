using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Validators;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllerTests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly UserValidator _validator = new();

        [Fact]
        public async Task Register_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange
            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);
            var registerUser = new RegisterUserRecord(
                "Login",
                "Invalid Email",
                "Password",
                "false");

            // Act
            var result = await controller.Register(registerUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsOk()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            _uowMock.Setup(u => u.AuthenticationRepository.RegisterUser(registerUser)).Returns(Task.CompletedTask);

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = await controller.Register(registerUser);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Register_ThrowsAnException()
        {
            // Arrange
            var registerUser = new RegisterUserRecord(
                "Login",
                "email@mail.ru",
                "Password",
                "false");
            _uowMock.Setup(u => u.AuthenticationRepository.RegisterUser(registerUser)).ThrowsAsync(new Exception());

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Register(registerUser));
        }

        [Fact]
        public async Task LoGin_ReturnsOk()
        {
            // Arrange
            var loginData = new LogInRequest(
                "Login",
                "email@mail.ru",
                "Password");
            var context = new HttpContextAccessor();
            var uowResult = new LogInResponseRecord(
                Guid.NewGuid(),
                "access token",
                "refresh token");

            _mapperMock.Setup(m => m.Map<RegisterUserRecord>(It.IsAny<LogInRequest>())).Returns((LogInRequest srs) => new RegisterUserRecord(srs.Login, srs.Email, srs.Password, "false"));
            _uowMock.Setup(u => u.AuthenticationRepository.LogInUser(loginData, context.HttpContext)).ReturnsAsync(uowResult);

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = await controller.LogIn(loginData);

            // Assert
            Assert.IsType<ActionResult<LogInResponseRecord>>(result);
        }

        [Fact]
        public async Task LoGin_ThrowsAnExceptions()
        {
            // Arrange
            var loginData = new LogInRequest(
                "LoginThatDoesntExist",
                "email_that_doesnt_exist@mail.ru",
                "Incorrect Password");
            var context = new HttpContextAccessor();
            var uowResult = new LogInResponseRecord(
                Guid.NewGuid(),
                "access token",
                "refresh token");

            _mapperMock.Setup(m => m.Map<RegisterUserRecord>(It.IsAny<LogInRequest>())).Returns((LogInRequest srs) => new RegisterUserRecord(srs.Login, srs.Email, srs.Password, "false"));
            _uowMock.Setup(u => u.AuthenticationRepository.LogInUser(loginData, context.HttpContext)).ThrowsAsync(new Exception());

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.LogIn(loginData));
        }

        [Fact]
        public async Task DeleteUser_ThrowsAnExceptionThatUserWithThatIdWasntFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.AuthenticationRepository.DeleteUser(id)).ThrowsAsync(new Exception("User with that ID wasn't found"));

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.DeleteUser(id));
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.AuthenticationRepository.DeleteUser(id)).Returns(Task.CompletedTask);

            var controller = new AuthenticationController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = await controller.DeleteUser(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}

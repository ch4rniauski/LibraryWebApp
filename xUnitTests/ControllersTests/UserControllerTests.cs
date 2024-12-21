using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Validators;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly UserValidator _validator = new();

        [Fact]
        public async Task GetUserInfo_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new UserInfoResponse(
                "Login",
                "email@mail.ru",
                false);

            _uowMock.Setup(u => u.UserRepository.GetUserInfo(id)).ReturnsAsync(user);

            var controller = new UserController(_uowMock.Object, _validator);

            // Act
            var result = await controller.GetUserInfo(id);

            // Assert
            Assert.IsType<ActionResult<UserInfoResponse>>(result);
        }

        [Fact]
        public async Task GetUserInfo_ThrowsAnExceptionThatUserWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.UserRepository.GetUserInfo(id)).ThrowsAsync(new Exception("Book with that ID wasn't found"));

            var controller = new UserController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetUserInfo(id));
        }

        [Fact]
        public async Task BorrowBook_ReturnsOk()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var request = new BorrowBookRequest(
                userId,
                bookId);

            _uowMock.Setup(u => u.UserRepository.BorrowBook(userId, bookId)).Returns(Task.CompletedTask);

            var controller = new UserController(_uowMock.Object, _validator);

            // Act
            var result = await controller.BorrowBook(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task BorrowBook_ThrowsAnException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var request = new BorrowBookRequest(
                userId,
                bookId);

            _uowMock.Setup(u => u.UserRepository.BorrowBook(userId, bookId)).ThrowsAsync(new Exception());

            var controller = new UserController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.BorrowBook(request));
        }
    }
}

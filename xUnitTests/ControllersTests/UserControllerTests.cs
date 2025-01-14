using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new();

        [Fact]
        public async Task GetUserInfo_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new UserInfoResponse(
                "Login",
                "email@mail.ru",
                false);

            _userServiceMock.Setup(u => u.GetUserInfo(id)).ReturnsAsync(user);

            var controller = new UserController(_userServiceMock.Object);

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

            _userServiceMock.Setup(u => u.GetUserInfo(id)).ThrowsAsync(new NotFoundException("User with that ID doesn't exist"));

            var controller = new UserController(_userServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.GetUserInfo(id));
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

            _userServiceMock.Setup(u => u.BorrowBook(userId, bookId)).Returns(Task.CompletedTask);

            var controller = new UserController(_userServiceMock.Object);

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

            _userServiceMock.Setup(u => u.BorrowBook(userId, bookId)).ThrowsAsync(new IncorrectDataException("That book is already borrowed"));

            var controller = new UserController(_userServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await controller.BorrowBook(request));
        }
    }
}

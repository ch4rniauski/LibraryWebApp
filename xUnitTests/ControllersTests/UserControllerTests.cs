using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.UserUseCases;
using Application.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IBorrowBookUseCase> _borrowBookUseCase = new();
        private readonly Mock<IGetUserInfoUseCase> _getUserInfoUseCase = new();

        [Fact]
        public async Task GetUserInfo_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new UserInfoResponse(
                "Login",
                "email@mail.ru",
                false);

            _getUserInfoUseCase.Setup(g => g.Execute(id)).ReturnsAsync(user);

            var controller = new UserController(_borrowBookUseCase.Object, _getUserInfoUseCase.Object);

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

            _getUserInfoUseCase.Setup(g => g.Execute(id)).ThrowsAsync(new NotFoundException("User with that ID doesn't exist"));

            var controller = new UserController(_borrowBookUseCase.Object, _getUserInfoUseCase.Object);

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

            _borrowBookUseCase.Setup(b => b.Execute(userId, bookId)).Returns(Task.CompletedTask);

            var controller = new UserController(_borrowBookUseCase.Object, _getUserInfoUseCase.Object);

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

            _borrowBookUseCase.Setup(b => b.Execute(userId, bookId)).ThrowsAsync(new IncorrectDataException("That book is already borrowed"));

            var controller = new UserController(_borrowBookUseCase.Object, _getUserInfoUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await controller.BorrowBook(request));
        }
    }
}

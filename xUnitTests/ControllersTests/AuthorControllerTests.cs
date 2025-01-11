using Domain.Abstractions.Records;
using Domain.Abstractions.Services;
using Domain.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllerTests
{
    public class AuthorControllerTests
    {
        private readonly Mock<IAuthorService> _authorServiceMock = new();

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.CreateAuthor(request)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act
            var result = await controller.Create(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Create_ThrowsAnExceptionThatAuthorWasntCreated()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.CreateAuthor(request)).ThrowsAsync(new CreationFailureException("Author wasn't created"));

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<CreationFailureException>(async () => await controller.Create(request));
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var list = new List<CreateAuthorRecord>();

            _authorServiceMock.Setup(u => u.GetAllAuthors()).ReturnsAsync(list);

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<ActionResult<List<CreateAuthorRecord>>>(result);
        }

        [Fact]
        public async Task Get_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new CreateAuthorRecord(
                "FirstName",
                "SecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.GetAuthorById(id)).ReturnsAsync(author);

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act
            var result = await controller.Get(id);

            // Assert
            Assert.IsType<ActionResult<CreateAuthorRecord>>(result);
        }

        [Fact]
        public async Task Get_ThrowsAnExceptionThatAuthorWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.GetAuthorById(id)).ThrowsAsync(new NotFoundException("Author with that ID doesn't exist"));

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Get(id));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _authorServiceMock.Setup(u => u.DeleteAutor(id)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ThrowsAnExceptionThatAuthorWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _authorServiceMock.Setup(u => u.DeleteAutor(id)).ThrowsAsync(new RemovalFailureException("Author with that ID wasn't deleted"));

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<RemovalFailureException>(async () => await controller.Delete(id));
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new UpdateAuthorRecord(
                id,
                "FirstName",
                "SecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.UpdateAuthor(author)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act
            var result = await controller.Update(author);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_ThrowsAnExceptionThatAuthorWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new UpdateAuthorRecord(
                id,
                "FirstName",
                "SecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _authorServiceMock.Setup(u => u.UpdateAuthor(author)).ThrowsAsync(new NotFoundException("Author with that ID doesn't exist"));

            var controller = new AuthorController(_authorServiceMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Update(author));
        }
    }
}

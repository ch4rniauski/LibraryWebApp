using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllersTests
{
    public class AuthorControllerTests
    {
        private readonly Mock<ICreateAuthorUseCase> _createAuthorUseCase = new();
        private readonly Mock<IDeleteAutorUseCase> _deleteAutorUseCase = new();
        private readonly Mock<IGetAllAuthorsUseCase> _getAllAuthorsUseCase = new();
        private readonly Mock<IGetAuthorByIdUseCase> _getAuthorByIdUseCase = new();
        private readonly Mock<IUpdateAuthorUseCase> _updateAuthorUseCase = new();

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _createAuthorUseCase.Setup(c => c.Execute(request)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _createAuthorUseCase.Setup(c => c.Execute(request)).ThrowsAsync(new CreationFailureException("Author wasn't created"));

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<CreationFailureException>(async () => await controller.Create(request));
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var list = new List<CreateAuthorRecord>();

            _getAllAuthorsUseCase.Setup(g => g.Execute()).ReturnsAsync(list);

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _getAuthorByIdUseCase.Setup(g => g.Execute(id)).ReturnsAsync(author);

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _getAuthorByIdUseCase.Setup(g => g.Execute(id)).ThrowsAsync(new NotFoundException("Author with that ID doesn't exist"));

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Get(id));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _deleteAutorUseCase.Setup(d => d.Execute(id)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _deleteAutorUseCase.Setup(d => d.Execute(id)).ThrowsAsync(new RemovalFailureException("Author with that ID wasn't deleted"));

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _updateAuthorUseCase.Setup(u => u.Execute(author)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

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

            _updateAuthorUseCase.Setup(u => u.Execute(author)).ThrowsAsync(new NotFoundException("Author with that ID doesn't exist"));

            var controller = new AuthorController(_createAuthorUseCase.Object,
                _deleteAutorUseCase.Object,
                _getAllAuthorsUseCase.Object,
                _getAuthorByIdUseCase.Object,
                _updateAuthorUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Update(author));
        }
    }
}

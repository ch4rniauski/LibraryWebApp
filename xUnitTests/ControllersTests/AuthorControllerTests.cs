using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Validators;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace xUnitTests.ControllerTests
{
    public class AuthorControllerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly AuthorValidator _validator = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _uowMock.Setup(u => u.AuthorRepository.CreateAuthor(request)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = await controller.Create(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(15, 10, 10));

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = await controller.Create(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
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

            _uowMock.Setup(u => u.AuthorRepository.CreateAuthor(request)).ThrowsAsync(new Exception("Author wasn't created"));

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Create(request));
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            // Arrange
            var list = new List<CreateAuthorRecord>();

            _uowMock.Setup(u => u.AuthorRepository.GetAllAuthors()).Returns(list);

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act
            var result = controller.GetAll();

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

            _uowMock.Setup(u => u.AuthorRepository.GetAuthor(id)).ReturnsAsync(author);

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

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

            _uowMock.Setup(u => u.AuthorRepository.GetAuthor(id)).ThrowsAsync(new Exception("Author with that id doesn't exist"));

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Get(id));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.AuthorRepository.DeleteAutor(id)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

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

            _uowMock.Setup(u => u.AuthorRepository.DeleteAutor(id)).ThrowsAsync(new Exception("Author with that id doesn't exist"));

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Delete(id));
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

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<UpdateAuthorRecord>())).Returns((UpdateAuthorRecord src) => new CreateAuthorRecord(
                src.FirstName,
                src.SecondName,
                src.Country,
                src.BirthDate));

            _uowMock.Setup(u => u.AuthorRepository.UpdateAuthor(author)).Returns(Task.CompletedTask);

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

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

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<UpdateAuthorRecord>())).Returns((UpdateAuthorRecord src) => new CreateAuthorRecord(
                src.FirstName,
                src.SecondName,
                src.Country,
                src.BirthDate));

            _uowMock.Setup(u => u.AuthorRepository.UpdateAuthor(author)).ThrowsAsync(new Exception("Author with that id doesn't exist"));

            var controller = new AuthorController(_uowMock.Object, _validator, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Update(author));
        }
    }
}

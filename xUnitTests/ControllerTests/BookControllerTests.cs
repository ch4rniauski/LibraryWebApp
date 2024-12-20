using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Validators;
using FluentValidation;
using FluentValidation.Results;
using Library.DataContext;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllerTests
{
    public class BookControllerTests
    {
        private readonly LibraryContext _context;

        public BookControllerTests()
        {
            _context = ContextGenerator.Generate();
        }

        [Fact]
        public async Task Create_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var book = new CreateBookRecord("","","","","","","", null,null);

            var controller = new BookController(uowMock.Object, new BookValidator());

            // Act
            var result = await controller.Create(book);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();

            var book = new CreateBookRecord(
                "978-3-16-148410-0", 
                "Title", 
                "Genre",
                "Description",
                null,
                null,
                null,
                null,
                null);

            uowMock.Setup(uow => uow.BookRepository.CreateBook(book)).Returns(Task.CompletedTask);


            var controller = new BookController(uowMock.Object, new BookValidator());

            // Act
            var result = await controller.Create(book);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}

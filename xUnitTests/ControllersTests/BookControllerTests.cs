using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;
using Domain.Validators;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllerTests
{
    public class BookControllerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly BookValidator _validator = new();

        [Fact]
        public async Task Create_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange
            var book = new CreateBookRecord(
                "InvalidISBN", 
                "Title",
                "Genre",
                "Description",
                null,
                null,
                null,
                null,
                null);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.Create(book);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOk()
        {
            // Arrange
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

            _uowMock.Setup(uow => uow.BookRepository.CreateBook(book)).Returns(Task.CompletedTask);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.Create(book);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Create_ThrowsAnExceptionThatAuthorWithThatNameDoesntExist()
        {
            // Arrange
            var book = new CreateBookRecord(
                "978-3-16-148410-0",
                "Title",
                "Genre",
                "Description",
                "AuthorThatDoesntExist",
                "AuthorThatDoesntExist",
                null,
                null,
                null);

            _uowMock.Setup(u => u.BookRepository.CreateBook(book)).ThrowsAsync(new Exception("Author with that name doesn't exist"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Create(book));
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            // Arrange
            var list = new List<GetBookRecord>();

            _uowMock.Setup(u => u.BookRepository.GetAll()).Returns(list);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsType<ActionResult<List<GetBookRecord>>>(result);
        }

        [Fact]
        public async Task GetById_ThrowsAnExceptionBookWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.GetBookById(id)).Throws(new Exception("Book with that ID wasn't found"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetById(id));
        }

        [Fact]
        public async Task GetById_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var book = new GetBookRecord(
                id,
                "978-3-16-148410-0",
                "Title",
                "Genre",
                "Description",
                null,
                null,
                null,
                null,
                null,
                null);

            _uowMock.Setup(u => u.BookRepository.GetBookById(id)).ReturnsAsync(book);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.GetById(id);

            // Assert
            Assert.IsType<ActionResult<GetBookRecord>>(result);
        }

        [Fact]
        public async Task GetByISBN_ThrowsAnExceptionBookWithThatISBNDoesntExist()
        {
            // Arrange
            var isbn = "978-3-16-148410-0";

            _uowMock.Setup(u => u.BookRepository.GetBookByISBN(isbn)).Throws(new Exception("Book with that ISBN wasn't found"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetByISBN(isbn));
        }

        [Fact]
        public async Task GetByISBN_ReturnsOk()
        {
            // Arrange
            var isbn = "978-3-16-148410-0";
            var book = new GetBookRecord(
                Guid.NewGuid(),
                isbn,
                "Title",
                "Genre",
                "Description",
                null,
                null,
                null,
                null,
                null,
                null);

            _uowMock.Setup(u => u.BookRepository.GetBookByISBN(isbn)).ReturnsAsync(book);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.GetByISBN(isbn);

            // Assert
            Assert.IsType<ActionResult<GetBookRecord>>(result);
        }

        [Fact]
        public async Task GetWithParams_ReturnOk()
        {
            // Arrange
            var request = new GetBookRequest(
                "BookTitleForSearch",
                "author");
            var list = new List<GetBookResponse>();

            _uowMock.Setup(u => u.BookRepository.GetBooksWithParams(request)).ReturnsAsync(list);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.GetWithParams(request);

            // Assert
            Assert.IsType<ActionResult<List<GetBookResponse>>>(result);
        }

        [Fact]
        public async Task Delete_ThrowsAnExceptionBookWithThatIdDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            
            _uowMock.Setup(u => u.BookRepository.DeleteBook(id)).ThrowsAsync(new Exception("Book with that ID wasn't found"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Delete(id));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.DeleteBook(id)).Returns(Task.CompletedTask);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_ThrowsAnException()
        {
            // Arrange
            var request = new CreateBookRecord(
                "978-3-16-148410-0",
                "Title",
                "Genre",
                null,
                "Author That Doesn't exist",
                "Author That Doesn't exist",
                null,
                null,
                null);
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.UpdateBook(request, id)).ThrowsAsync(new Exception());

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.Update(request, id));
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            // Arrange
            var request = new CreateBookRecord(
                "978-3-16-148410-0",
                "Title",
                "Genre",
                null,
                "Author First Name",
                "Author Second Name",
                null,
                null,
                null);
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.UpdateBook(request, id)).Returns(Task.CompletedTask);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.Update(request, id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetBooksByUserId_ReturnsOk()
        {
            // Arrange
            var list = new List<GetBookRecord>();
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.GetBooksByUserId(id)).ReturnsAsync(list);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.GetBooksByUserId(id);

            // Assert
            Assert.IsType<ActionResult<List<GetBookRecord>>>(result);
        }

        [Fact]
        public async Task GetBooksByUserId_ThrowsAnExceptionUserWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.GetBooksByUserId(id)).ThrowsAsync(new Exception("User with that ID doesn't exist"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetBooksByUserId(id));
        }

        [Fact]
        public async Task ReturnBook_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.ReturnBook(id)).Returns(Task.CompletedTask);

            var controller = new BookController(_uowMock.Object, _validator);

            // Act
            var result = await controller.ReturnBook(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ReturnBook_ThrowsAnException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _uowMock.Setup(u => u.BookRepository.ReturnBook(id)).ThrowsAsync(new Exception("User with that ID doesn't exist"));

            var controller = new BookController(_uowMock.Object, _validator);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.ReturnBook(id));
        }
    }
}

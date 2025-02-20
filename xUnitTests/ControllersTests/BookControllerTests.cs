﻿using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using LibraryWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace xUnitTests.ControllersTests
{
    public class BookControllerTests
    {
        private readonly Mock<ICreateBookUseCase> _createBookUseCase = new();
        private readonly Mock<IDeleteBookUseCase> _deleteBookUseCase = new();
        private readonly Mock<IGetAllBooksUseCase> _getAllBooksUseCase = new();
        private readonly Mock<IGetBookByIdUseCase> _getBookByIdUseCase = new();
        private readonly Mock<IGetBookByISBNUseCase> _getBookByISBNUseCase = new();
        private readonly Mock<IGetBooksByUserIdUseCase> _getBooksByUserIdUseCase = new();
        private readonly Mock<IGetBooksWithParamsUseCase> _getBooksWithParamsUseCase = new();
        private readonly Mock<IReturnBookUseCase> _returnBookUseCase = new();
        private readonly Mock<IUpdateBookUseCase> _updateBookUseCase = new();

        [Fact]
        public async Task Create_ThrowsAnExceptionWithValidationErrors()
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
            _createBookUseCase.Setup(c => c.Execute(book)).ThrowsAsync(new IncorrectDataException("Incorrect data"));
            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await controller.Create(book));
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

            _createBookUseCase.Setup(c => c.Execute(book)).Returns(Task.CompletedTask);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _createBookUseCase.Setup(c => c.Execute(book)).ThrowsAsync(new NotFoundException("Author with that name doesn't exist"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Create(book));
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var list = new List<GetBookRecord>();

            _getAllBooksUseCase.Setup(g => g.Execute()).ReturnsAsync(list);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<ActionResult<List<GetBookRecord>?>>(result);
        }

        [Fact]
        public async Task GetById_ThrowsAnExceptionBookWithThatIDDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _getBookByIdUseCase.Setup(g => g.Execute(id)).ThrowsAsync(new NotFoundException("Book with that ID wasn't found"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.GetById(id));
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

            _getBookByIdUseCase.Setup(g => g.Execute(id)).ReturnsAsync(book);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _getBookByISBNUseCase.Setup(g => g.Execute(isbn)).Throws(new NotFoundException("Book with that ISBN wasn't found"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.GetByISBN(isbn));
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

            _getBookByISBNUseCase.Setup(g => g.Execute(isbn)).ReturnsAsync(book);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _getBooksWithParamsUseCase.Setup(g => g.Execute(request)).ReturnsAsync(list);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _deleteBookUseCase.Setup(d => d.Execute(id)).ThrowsAsync(new NotFoundException("Book with that ID wasn't found"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Delete(id));
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _deleteBookUseCase.Setup(d => d.Execute(id)).Returns(Task.CompletedTask);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _updateBookUseCase.Setup(u => u.Execute(request, id)).ThrowsAsync(new NotFoundException("Book with that ID wasn't found"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.Update(request, id));
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

            _updateBookUseCase.Setup(u => u.Execute(request, id)).Returns(Task.CompletedTask);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _getBooksByUserIdUseCase.Setup(g => g.Execute(id)).ReturnsAsync(list);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _getBooksByUserIdUseCase.Setup(g => g.Execute(id)).ThrowsAsync(new NotFoundException("User with that ID doesn't exist"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.GetBooksByUserId(id));
        }

        [Fact]
        public async Task ReturnBook_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            _returnBookUseCase.Setup(r => r.Execute(id)).Returns(Task.CompletedTask);

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

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

            _returnBookUseCase.Setup(r => r.Execute(id)).ThrowsAsync(new NotFoundException("Book with that ID doesn't exist"));

            var controller = new BookController(_createBookUseCase.Object,
                _deleteBookUseCase.Object,
                _getAllBooksUseCase.Object,
                _getBookByIdUseCase.Object,
                _getBookByISBNUseCase.Object,
                _getBooksByUserIdUseCase.Object,
                _getBooksWithParamsUseCase.Object,
                _returnBookUseCase.Object,
                _updateBookUseCase.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.ReturnBook(id));
        }
    }
}

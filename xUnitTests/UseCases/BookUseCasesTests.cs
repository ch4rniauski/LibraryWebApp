using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Exceptions.CustomExceptions;
using Application.UseCases.BookUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;
using Moq;

namespace xUnitTests.UseCases
{
    public class BookUseCasesTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<CreateBookRecord>> _validatorMock = new();

        [Fact]
        public async Task CreateBookUseCase_ThrowsAnExceptionWithValidationErrors()
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

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ThrowsAsync(new IncorrectDataException("Incorrect data"));

            var createBookUseCase = new CreateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await createBookUseCase.Execute(request));
        }

        [Fact]
        public async Task CreateBookUseCase_ThrowsAnExceptionThatAuthorWithThatNameDoesntExist()
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
            AuthorEntity? author = null;
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _mapperMock.Setup(m => m.Map<BookEntity>(request)).Returns((CreateBookRecord src) => new BookEntity
            {
                Title = src.Title,
                ISBN = src.ISBN,
                Genre = src.Genre,
                Description = src.Description,
                AuthorFirstName = src.AuthorFirstName,
                AuthorSecondName = src.AuthorSecondName,
                ImageData = src.ImageData,
                TakenAt = src.TakenAt,
                DueDate = src.DueDate,
            });
            _unitOfWork.Setup(u => u.AuthorRepository.GetByFirstName(request.AuthorFirstName!)).ReturnsAsync(author);
            _unitOfWork.Setup(u => u.AuthorRepository.GetBySecondName(request.AuthorSecondName!)).ReturnsAsync(author);

            var createBookUseCase = new CreateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await createBookUseCase.Execute(request));
        }

        [Fact]
        public async Task CreateBookUseCase_Done()
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
            var author = new AuthorEntity
            {
                FirstName = "Author First Name",
                SecondName = "Author Second Name"
            };
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _mapperMock.Setup(m => m.Map<BookEntity>(request)).Returns((CreateBookRecord src) => new BookEntity
            {
                Title = src.Title,
                ISBN = src.ISBN,
                Genre = src.Genre,
                Description = src.Description,
                AuthorFirstName = src.AuthorFirstName,
                AuthorSecondName = src.AuthorSecondName,
                ImageData = src.ImageData,
                TakenAt = src.TakenAt,
                DueDate = src.DueDate,
            });
            _unitOfWork.Setup(u => u.AuthorRepository.GetByFirstName(request.AuthorFirstName!)).ReturnsAsync(author);
            _unitOfWork.Setup(u => u.AuthorRepository.GetBySecondName(request.AuthorSecondName!)).ReturnsAsync(author);
            _unitOfWork.Setup(u => u.BookRepository.Create(It.IsAny<BookEntity>())).ReturnsAsync(true);

            var createBookUseCase = new CreateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act
            await createBookUseCase.Execute(request);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteBookUseCase_ThrowsAnExceptionThatAuthorWithThatIdDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ThrowsAsync(new NotFoundException("Book with that ID wasn't found"));

            var deleteBookUseCase = new DeleteBookUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await deleteBookUseCase.Execute(id));
        }

        [Fact]
        public async Task DeleteBookUseCase_Done()
        {
            // Arrange
            var bookEntity = new BookEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);
            _unitOfWork.Setup(u => u.BookRepository.Delete(bookEntity)).Returns(true);

            var deleteBookUseCase = new DeleteBookUseCase(_unitOfWork.Object);

            // Act
            await deleteBookUseCase.Execute(id);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task GetAllBooks_Done()
        {
            // Arrange
            var list = new List<BookEntity>();

            _unitOfWork.Setup(u => u.BookRepository.GetAll()).ReturnsAsync(list);

            _mapperMock.Setup(m => m.Map<List<GetBookRecord>>(list)).Returns(new List<GetBookRecord>());

            var getAllBooksUseCase = new GetAllBooksUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getAllBooksUseCase.Execute();

            // Assert
            Assert.IsType<List<GetBookRecord>>(result);
        }

        [Fact]
        public async Task GetBookByIdUseCase_ThrowsNotFoundException()
        {
            // Arrange
            BookEntity? bookEntity = null;
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);

            var getBookByIdUseCase = new GetBookByIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await getBookByIdUseCase.Execute(id));
        }

        [Fact]
        public async Task GetBookByIdUseCase_Done()
        {
            // Arrange
            var bookEntity = new BookEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);

            _mapperMock.Setup(m => m.Map<GetBookRecord>(It.IsAny<BookEntity>())).Returns((BookEntity src) => new GetBookRecord(
                src.Id,
                src.ISBN,
                src.Title,
                src.Genre,
                src.Description,
                src.AuthorFirstName,
                src.AuthorSecondName,
                src.ImageData,
                src.TakenAt,
                src.DueDate,
                src.UserId));

            var getBookByIdUseCase = new GetBookByIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getBookByIdUseCase.Execute(id);

            // Assert
            Assert.IsType<GetBookRecord>(result);
        }

        [Fact]
        public async Task GetBookByIsbnUseCase_ThrowsNotFoundException()
        {
            // Arrange
            BookEntity? bookEntity = null;

            _unitOfWork.Setup(u => u.BookRepository.GetBookByISBN("isbn")).ReturnsAsync(bookEntity);

            var getBookByIsbnUseCase = new GetBookByISBNUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await getBookByIsbnUseCase.Execute("isbn"));
        }

        [Fact]
        public async Task GetBookByIsbnUseCase_Done()
        {
            // Arrange
            var bookEntity = new BookEntity();

            _unitOfWork.Setup(u => u.BookRepository.GetBookByISBN("isbn")).ReturnsAsync(bookEntity);

            _mapperMock.Setup(m => m.Map<GetBookRecord>(It.IsAny<BookEntity>())).Returns((BookEntity src) => new GetBookRecord(
                src.Id,
                src.ISBN,
                src.Title,
                src.Genre,
                src.Description,
                src.AuthorFirstName,
                src.AuthorSecondName,
                src.ImageData,
                src.TakenAt,
                src.DueDate,
                src.UserId));

            var getBookByIsbnUseCase = new GetBookByISBNUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getBookByIsbnUseCase.Execute("isbn");

            // Assert
            Assert.IsType<GetBookRecord>(result);
        }

        [Fact]
        public async Task GetBooksByUserIdUseCase_ThrowsNotFoundException()
        {
            // Arrange
            UserEntity? userEntity = null;
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(userEntity);

            var getBooksByUserIdUseCase = new GetBooksByUserIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await getBooksByUserIdUseCase.Execute(id));
        }

        [Fact]
        public async Task GetBooksByUserIdUseCase_Done()
        {
            // Arrange
            var userEntity = new UserEntity();
            var id = Guid.NewGuid();
            var list = new List<BookEntity>();

            _unitOfWork.Setup(u => u.UserRepository.GetById(id)).ReturnsAsync(userEntity);
            _unitOfWork.Setup(u => u.BookRepository.GetBooksByUserId(id)).ReturnsAsync(list);

            _mapperMock.Setup(m => m.Map<List<GetBookRecord>>(list)).Returns(new List<GetBookRecord>());

            var getBooksByUserIdUseCase = new GetBooksByUserIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getBooksByUserIdUseCase.Execute(id);

            // Assert
            Assert.IsType<List<GetBookRecord>>(result);
        }

        [Fact]
        public async Task GetBooksWithParamsUseCase_Done()
        {
            // Arrange
            var request = new GetBookRequest("search", "genre");
            var list = new List<BookEntity>();

            _unitOfWork.Setup(u => u.BookRepository.GetAll()).ReturnsAsync(list);

            _mapperMock.Setup(m => m.Map<List<GetBookRecord>>(list)).Returns(new List<GetBookRecord>());
            _mapperMock.Setup(m => m.Map<List<GetBookResponse>>(list)).Returns(new List<GetBookResponse>());

            var getBooksWithParamsUseCase = new GetBooksWithParamsUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getBooksWithParamsUseCase.Execute(request);

            // Assert
            Assert.IsType<List<GetBookResponse>>(result);
        }

        [Fact]
        public async Task ReturnBookUseCase_ThrowsNotFoundException()
        {
            // Arrange
            BookEntity? bookEntity = null;
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);

            var returnBookUseCase = new ReturnBookUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await returnBookUseCase.Execute(id));
        }

        [Fact]
        public async Task ReturnBookUseCase_Done()
        {
            // Arrange
            var bookEntity = new BookEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);

            var returnBookUseCase = new ReturnBookUseCase(_unitOfWork.Object);
            // Act
            await returnBookUseCase.Execute(id);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task UpdateBookUseCase_ThrowsAnExceptionWithValidationErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
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

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ThrowsAsync(new IncorrectDataException("Incorrect data"));

            var updateBookUseCase = new UpdateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await updateBookUseCase.Execute(request, id));
        }

        [Fact]
        public async Task UpdateBookUseCase_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
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
            BookEntity? bookEntity = null;

            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookEntity);

            var updateBookUseCase = new UpdateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await updateBookUseCase.Execute(request, id));
        }

        [Fact]
        public async Task UpdateBookUseCase_Done()
        {
            // Arrange
            var id = Guid.NewGuid();
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
            var bookToUpdate = new BookEntity();
            var author = new AuthorEntity
            {
                FirstName = "Author First Name",
                SecondName = "Author Second Name"
            };

            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _mapperMock.Setup(m => m.Map(request, bookToUpdate)).Returns(bookToUpdate);
            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _unitOfWork.Setup(u => u.BookRepository.GetById(id)).ReturnsAsync(bookToUpdate);
            _unitOfWork.Setup(u => u.AuthorRepository.GetByFirstName(request.AuthorFirstName!)).ReturnsAsync(author);
            _unitOfWork.Setup(u => u.AuthorRepository.GetBySecondName(request.AuthorSecondName!)).ReturnsAsync(author);

            var updateBookUseCase = new UpdateBookUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act
            await updateBookUseCase.Execute(request, id);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}

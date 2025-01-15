using Application.Abstractions.Records;
using Application.Exceptions.CustomExceptions;
using Application.UseCases.UserUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using Moq;

namespace xUnitTests.UseCases
{
    public class UserUseCasesTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task BorrowBookUseCase_ThrowsNotFoundException_UserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            UserEntity? user = null;

            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);

            var borrowBookUseCase = new BorrowBookUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await borrowBookUseCase.Execute(userId, bookId));
        }

        [Fact]
        public async Task BorrowBookUseCase_ThrowsNotFoundException_BookNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var user = new UserEntity();
            BookEntity? book = null;

            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);
            _unitOfWork.Setup(u => u.BookRepository.GetById(bookId)).ReturnsAsync(book);

            var borrowBookUseCase = new BorrowBookUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await borrowBookUseCase.Execute(userId, bookId));
        }

        [Fact]
        public async Task BorrowBookUseCase_ThrowsIncorrectDataException_BookIsAlreadyBorrowed()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var user = new UserEntity();
            var book = new BookEntity
            {
                UserId = Guid.NewGuid(),
            };

            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);
            _unitOfWork.Setup(u => u.BookRepository.GetById(bookId)).ReturnsAsync(book);

            var borrowBookUseCase = new BorrowBookUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await borrowBookUseCase.Execute(userId, bookId));
        }

        [Fact]
        public async Task BorrowBookUseCase_Done()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var user = new UserEntity();
            var book = new BookEntity
            {
                UserId = null,
            };

            user.Books = new List<BookEntity>();
            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);
            _unitOfWork.Setup(u => u.BookRepository.GetById(bookId)).ReturnsAsync(book);

            var borrowBookUseCase = new BorrowBookUseCase(_unitOfWork.Object);

            // Act
            await borrowBookUseCase.Execute(userId, bookId);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task GetUserInfoUseCase_ThrowsNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            UserEntity? user = null;

            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);

            var getUserInfoBookUseCase = new GetUserInfoUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await getUserInfoBookUseCase.Execute(userId));
        }

        [Fact]
        public async Task GetUserInfoUseCase_Done()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new UserEntity();

            _unitOfWork.Setup(u => u.UserRepository.GetById(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserInfoResponse>(user)).Returns((UserEntity src) => new UserInfoResponse(
                src.Login,
                src.Email,
                src.IsAdmin));

            var getUserInfoBookUseCase = new GetUserInfoUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getUserInfoBookUseCase.Execute(userId);

            // Assert
            Assert.IsType<UserInfoResponse>(result);
        }
    }
}

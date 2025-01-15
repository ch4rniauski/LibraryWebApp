using Application.Abstractions.Requests;
using Application.Exceptions.CustomExceptions;
using Application.UseCases.AuthorUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;
using Domain.Entities;
using FluentValidation;
using Moq;

namespace xUnitTests.UseCases
{
    public class AuthorUseCasesTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<CreateAuthorRecord>> _validatorMock = new();

        [Fact]
        public async Task CreateAuthorUseCase_ThrowsAnExceptionWithValidationErrors()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ThrowsAsync(new IncorrectDataException("Incorrect data"));

            var createAuthorUseCase = new CreateAuthorUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await createAuthorUseCase.Execute(request));
        }

        [Fact]
        public async Task CreateAuthorUseCase_Done()
        {
            // Arrange
            var request = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _mapperMock.Setup(m => m.Map<AuthorEntity>(It.IsAny<CreateAuthorRecord>())).Returns((CreateAuthorRecord src) => new AuthorEntity
            {
                FirstName = src.FirstName,
                SecondName = src.SecondName,
                Country = src.Country,
                BirthDate = src.BirthDate,
            });

            _validatorMock.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);

            _unitOfWork.Setup(u => u.AuthorRepository.Create(It.IsAny<AuthorEntity>())).ReturnsAsync(true);

            var createAuthorUseCase = new CreateAuthorUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act
            await createAuthorUseCase.Execute(request);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteAuthorUseCase_ThrowsAnExceptionThatAuthorWithThatIdDoesntExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ThrowsAsync(new NotFoundException("Author with that ID doesn't exist"));

            var deleteAuthorUseCase = new DeleteAutorUseCase(_unitOfWork.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await deleteAuthorUseCase.Execute(id));
        }

        [Fact]
        public async Task DeleteAuthorUseCase_Done()
        {
            // Arrange
            var authorEntity = new AuthorEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ReturnsAsync(authorEntity);
            _unitOfWork.Setup(u => u.AuthorRepository.Delete(authorEntity)).Returns(true);

            var deleteAuthorUseCase = new DeleteAutorUseCase(_unitOfWork.Object);

            // Act
            await deleteAuthorUseCase.Execute(id);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public async Task GetAllAuthorsUseCase_Done()
        {
            // Arrange
            var list = new List<AuthorEntity>();
            var listToReturn = new List<CreateAuthorRecord>();

            _unitOfWork.Setup(u => u.AuthorRepository.GetAll()).ReturnsAsync(list);
            _mapperMock.Setup(m => m.Map<List<CreateAuthorRecord>>(It.IsAny<List<AuthorEntity>>())).Returns(listToReturn);

            var getAllAuthorsUseCase = new GetAllAuthorsUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getAllAuthorsUseCase.Execute();

            // Assert
            Assert.IsType<List<CreateAuthorRecord>>(result);
        }

        [Fact]
        public async Task GetAuthorByIdUseCase_ThrowsNotFoundException()
        {
            // Arrange
            AuthorEntity? authorEntity = null;
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ReturnsAsync(authorEntity);
            
            var getAuthorByIdUseCase = new GetAuthorByIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await getAuthorByIdUseCase.Execute(id));
        }

        [Fact]
        public async Task GetAuthorByIdUseCase_Done()
        {
            // Arrange
            var authorEntity = new AuthorEntity();
            var id = Guid.NewGuid();

            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ReturnsAsync(authorEntity);

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<AuthorEntity>())).Returns((AuthorEntity src) => new CreateAuthorRecord(
                src.FirstName,
                src.SecondName,
                src.Country,
                src.BirthDate));
            
            var getAuthorByIdUseCase = new GetAuthorByIdUseCase(_unitOfWork.Object, _mapperMock.Object);

            // Act
            var result = await getAuthorByIdUseCase.Execute(id);

            // Assert
            Assert.IsType<CreateAuthorRecord>(result);
        }

        [Fact]
        public async Task UpdateAuthorUseCase_ThrowsAnExceptionWithValidationErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new UpdateAuthorRecord(
                 id,
                 "FirstName",
                 "SecondName",
                 "Country",
                 new DateOnly(2000, 10, 10));
            var authorToValidate = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<UpdateAuthorRecord>())).Returns(authorToValidate);

            _validatorMock.Setup(v => v.ValidateAsync(authorToValidate, It.IsAny<CancellationToken>())).ThrowsAsync(new IncorrectDataException("Incorrect data"));

            var updateAuthorUseCase = new UpdateAuthorUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<IncorrectDataException>(async () => await updateAuthorUseCase.Execute(author));
        }
        
        [Fact]
        public async Task UpdateAuthorUseCase_ThrowsNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new UpdateAuthorRecord(
                 id,
                 "FirstName",
                 "SecondName",
                 "Country",
                 new DateOnly(2000, 10, 10));
            var authorToValidate = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));
            AuthorEntity? authorToUpdate = null;
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<UpdateAuthorRecord>())).Returns(authorToValidate);
            _validatorMock.Setup(v => v.ValidateAsync(authorToValidate, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ReturnsAsync(authorToUpdate);

            var updateAuthorUseCase = new UpdateAuthorUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await updateAuthorUseCase.Execute(author));
        }

        [Fact]
        public async Task UpdateAuthorUseCase_Done()
        {
            // Arrange
            var id = Guid.NewGuid();
            var author = new UpdateAuthorRecord(
                 id,
                 "FirstName",
                 "SecondName",
                 "Country",
                 new DateOnly(2000, 10, 10));
            var authorToValidate = new CreateAuthorRecord(
                "AuthorFirstName",
                "AuthorSecondName",
                "Country",
                new DateOnly(2000, 10, 10));
            var authorToUpdate = new AuthorEntity();
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _mapperMock.Setup(m => m.Map<CreateAuthorRecord>(It.IsAny<UpdateAuthorRecord>())).Returns(authorToValidate);
            _mapperMock.Setup(m => m.Map(author, authorToUpdate)).Returns(authorToUpdate);
            _validatorMock.Setup(v => v.ValidateAsync(authorToValidate, It.IsAny<CancellationToken>())).ReturnsAsync(validationResult.Object);
            _unitOfWork.Setup(u => u.AuthorRepository.GetById(id)).ReturnsAsync(authorToUpdate);

            var updateAuthorUseCase = new UpdateAuthorUseCase(_unitOfWork.Object, _mapperMock.Object, _validatorMock.Object);

            // Act
            await updateAuthorUseCase.Execute(author);

            // Assert
            _unitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}

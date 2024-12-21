using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;
using Domain.Models;
using Library.DataContext;
using Library.DataContext.Repositories;
using Microsoft.VisualBasic;
using Moq;

namespace xUnitTests.RepositoriesTests
{
    public class BookRepositoryTests
    {
        private readonly LibraryContext _context = ContextGenerator.Generate();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            // Arranges
            var bookId= Guid.NewGuid();

            _context.Books.Add(new BookEntity
            {
                Id = bookId,
                ISBN = "978-3-16-148410-0",
                Title = "Title",
                Genre = "Genre",
                Description = "Description",
                AuthorFirstName = null,
                AuthorSecondName = null,
                AuthorId = null,
                Author= null,
                TakenAt = null,
                DueDate = null,
                User = null,
                UserId = null,
                ImageURL = null
            });
            _context.SaveChanges();

            var bookToCompare = new GetBookRecord(
                bookId,
                "978-3-16-148410-0",
                "Title",
                "Genre",
                "Description",
                null,
                null,
                null,
                null,
                null,
                null
                );

            _mapperMock.Setup(m => m.Map<GetBookRecord>(It.IsAny<BookEntity>())).Returns((BookEntity src) => new GetBookRecord(
                src.Id,
                src.ISBN,
                src.Title,
                src.Genre,
                src.Description,
                src.AuthorFirstName,
                src.AuthorSecondName,
                src.ImageURL,
                src.TakenAt,
                src.DueDate,
                src.UserId));

            var repository = new BookRepository(_context, _mapperMock.Object);

            // Act
            var result = await repository.GetBookById(bookId);

            // Assert
            Assert.Equal(bookToCompare, result);
        }

        [Fact]
        public async Task GetBookById_ThrowsAnExceptionBookWithThatIDDoesntExist()
        {
            // Arranges
            _context.Books.Add(new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = "978-3-16-148410-0",
                Title = "Title",
                Genre = "Genre",
                Description = "Description",
                AuthorFirstName = null,
                AuthorSecondName = null,
                AuthorId = null,
                Author = null,
                TakenAt = null,
                DueDate = null,
                User = null,
                UserId = null,
                ImageURL = null
            });
            _context.SaveChanges();

            var repository = new BookRepository(_context, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await repository.GetBookById(Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateBook_Done()
        {
            // Arranges
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

            _mapperMock.Setup(m => m.Map<BookEntity>(It.IsAny<CreateBookRecord>())).Returns((CreateBookRecord src) => new BookEntity
            {
                ISBN = src.ISBN,
                Title = src.Title,
                Genre = src.Genre,
                Description = src.Description,
                AuthorFirstName = src.AuthorFirstName,
                AuthorSecondName = src.AuthorSecondName,
                ImageURL = src.ImageURL,
                TakenAt = src.TakenAt,
                DueDate = src.DueDate,
            });

            var repository = new BookRepository(_context, _mapperMock.Object);

            // Act
            await repository.CreateBook(book);
            _context.SaveChanges();

            // Assert
            Assert.NotEmpty(_context.Books);
        }

        [Fact]
        public async Task CreateBook_ThrowsAnExceptionThatAuthorWithThatNameDoesntExist()
        {
            // Arranges
            var book = new CreateBookRecord(
                "978-3-16-148410-0",
                "Title",
                "Genre",
                "Description",
                "FirstName",
                "SecondName",
                null,
                null,
                null);

            _context.Auhtors.Add(new AuthorEntity
            {
                FirstName = "NameThatDoesntExist",
                SecondName = "NameThatDoesntExist",
                BirthDate = new DateOnly(2000, 10, 10),
                Books = null,
                Country = "Poland",
                Id = Guid.NewGuid(),
            });
            _context.SaveChanges();

            _mapperMock.Setup(m => m.Map<BookEntity>(It.IsAny<CreateBookRecord>())).Returns((CreateBookRecord src) => new BookEntity
            {
                ISBN = src.ISBN,
                Title = src.Title,
                Genre = src.Genre,
                Description = src.Description,
                AuthorFirstName = src.AuthorFirstName,
                AuthorSecondName = src.AuthorSecondName,
                ImageURL = src.ImageURL,
                TakenAt = src.TakenAt,
                DueDate = src.DueDate,
            });

            var repository = new BookRepository(_context, _mapperMock.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(async () => await repository.CreateBook(book));
        }
    }
}

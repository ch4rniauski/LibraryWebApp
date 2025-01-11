using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;
using Library.DataContext;
using Library.DataContext.Repositories;
using Moq;

namespace xUnitTests.RepositoriesTests
{
    public class BookRepositoryTests
    {
        private readonly LibraryContext _context = ContextGenerator.Generate();

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            // Arranges
            var bookId = Guid.NewGuid();
            var bookEntity = new BookEntity
            {
                Id = bookId,
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
                ImageData = null
            };

            _context.Books.Add(bookEntity);
            _context.SaveChanges();
    
            var repository = new BookRepository(_context);

            // Act
            var result = await repository.GetById(bookId);

            // Assert
            Assert.Equal(bookEntity, result);
        }

        [Fact]
        public async Task CreateBook_Done()
        {
            // Arranges
            var bookEntity = new BookEntity
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
                ImageData = null
            };

            var repository = new BookRepository(_context);

            // Act
            await repository.Create(bookEntity);
            _context.SaveChanges();

            // Assert
            Assert.NotEmpty(_context.Books);
        }
    }
}

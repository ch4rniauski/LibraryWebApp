using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task CreateBook(BookEntity book);
        Task<BookEntity?> GetBookById(Guid id);
        Task<BookEntity?> GetBookByISBN(string ISBN);
        Task<List<BookEntity>?> GetAllBooks();
        void DeleteBook(BookEntity book);
        Task<List<BookEntity>?> GetBooksByUserId(Guid id);
    }
}

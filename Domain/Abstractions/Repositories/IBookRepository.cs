using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task CreateBook(BookRecord book);
        Task<BookRecord> GetBook(Guid id);
        Task<List<BookRecord>> GetAllBooks();
        Task<bool> UpdateBook(BookRecord book);
        Task<bool> DeleteBook(BookRecord book);
    }
}

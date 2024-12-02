using Domain.Abstractions.Records;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<ActionResult> CreateBook(BookRecord book);
        Task<ActionResult<BookRecord>> GetBook(Guid id);
        Task<ActionResult<List<BookRecord>>> GetAllBooks();
        Task<ActionResult> UpdateBook(BookRecord book);
        Task<ActionResult> DeleteBook(BookRecord book);
    }
}

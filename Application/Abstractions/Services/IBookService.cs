using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Domain.Abstractions.Records;

namespace Application.Abstractions.Services
{
    public interface IBookService
    {
        Task CreateBook(CreateBookRecord book);
        Task<GetBookRecord> GetBookById(Guid id);
        Task<GetBookRecord> GetBookByISBN(string ISBN);
        Task<List<GetBookRecord>?> GetAllBooks();
        Task UpdateBook(CreateBookRecord book, Guid id);
        Task DeleteBook(Guid id);
        Task<List<GetBookRecord>?> GetBooksByUserId(Guid id);
        Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request);
        Task ReturnBook(Guid id);
    }
}

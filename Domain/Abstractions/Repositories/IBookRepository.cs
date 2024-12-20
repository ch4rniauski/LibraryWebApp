using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task CreateBook(CreateBookRecord book);
        Task<GetBookRecord> GetBookById(Guid id);
        Task<GetBookRecord> GetBookByISBN(string ISBN);
        List<GetBookRecord>? GetAllBooks();
        Task UpdateBook(CreateBookRecord book, Guid id);
        Task DeleteBook(Guid id);
        List<GetBookRecord>? GetBooksByUserId(Guid id);
        Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request);
        Task ReturnBook(Guid id);
    }
}

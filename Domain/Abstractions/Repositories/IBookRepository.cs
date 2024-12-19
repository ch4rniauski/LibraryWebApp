using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<string?> CreateBook(CreateBookRecord book);
        Task<GetBookRecord?> GetBookById(Guid id);
        Task<GetBookRecord?> GetBookByISBN(string ISBN);
        List<GetBookRecord>? GetAllBooks();
        Task<string?> UpdateBook(CreateBookRecord book, Guid id);
        Task<bool> DeleteBook(Guid id);
        List<GetBookRecord>? GetBooksByUserId(Guid id);
        Task<List<GetBookResponse>?> GetBooksWithParams(GetBookRequest request);
        Task<bool> ReturnBook(Guid id);
    }
}

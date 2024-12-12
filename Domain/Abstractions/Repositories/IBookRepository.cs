using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<string?> CreateBook(CreateBookRecord book);
        Task<UpdateBookRecord?> GetBookById(Guid id);
        Task<UpdateBookRecord?> GetBookByISBN(string ISBN);
        List<GetBookRecord> GetAllBooks();
        Task<bool> UpdateBook(UpdateBookRecord book);
        Task<bool> DeleteBook(Guid id);
    }
}

using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.DataContext.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task CreateBook(BookRecord book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBook(BookRecord book)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookRecord>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<BookRecord> GetBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBook(BookRecord book)
        {
            throw new NotImplementedException();
        }
    }
}

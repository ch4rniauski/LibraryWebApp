using Domain.Abstractions.Records;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.DataContext.Repositories
{
    internal class BookRepository : IBookRepository
    {
        public Task<ActionResult> CreateBook(BookRecord book)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteBook(BookRecord book)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<BookRecord>>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<BookRecord>> GetBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateBook(BookRecord book)
        {
            throw new NotImplementedException();
        }
    }
}

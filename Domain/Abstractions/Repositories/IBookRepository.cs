﻿using Domain.Abstractions.Records;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<bool> CreateBook(BookRecord book);
        Task<BookRecord?> GetBook(Guid id);
        List<BookRecord> GetAllBooks();
        Task<bool> UpdateBook(BookRecord book);
        Task<bool> DeleteBook(Guid id);
    }
}

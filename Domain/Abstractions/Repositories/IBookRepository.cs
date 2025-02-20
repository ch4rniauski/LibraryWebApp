﻿using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IBookRepository : IGenericRepository<BookEntity>
    {
        Task<BookEntity?> GetBookByISBN(string ISBN);
        Task<List<BookEntity>?> GetBooksByUserId(Guid id);
        Task<List<BookEntity>?> SortByGenreAndSearch(string search);
        Task<List<BookEntity>?> SortByAuthorAndSearch(string search);
    }
}

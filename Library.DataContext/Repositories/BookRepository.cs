﻿using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext.Repositories
{
    public class BookRepository : GenericRepository<BookEntity>, IBookRepository
    {
        private readonly LibraryContext _db;

        public BookRepository(LibraryContext db) : base(db)
        {
            _db = db;
        }

        public async Task<BookEntity?> GetBookByISBN(string ISBN)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);

            return book;
        }

        public async Task<List<BookEntity>?> GetBooksByUserId(Guid id)
        {
            var books = await _db.Books
                .Where(b => b.UserId == id)
                .ToListAsync();

            return books;
        }

        public async Task<List<BookEntity>?> SortByAuthorAndSearch(string search)
        {
            var books = await _db.Books
                .Where(b => b.Title.Contains(search))
                .OrderBy(b => b.Author.FirstName)
                .ThenBy(b => b.Author.SecondName)
                .ToListAsync();

            return books;
        }

        public async Task<List<BookEntity>?> SortByGenreAndSearch(string search)
        {
            var books = await _db.Books
                .Where(b => b.Title.Contains(search))
                .OrderBy(b => b.Genre)
                .ToListAsync();

            return books;
        }

        public override async Task<BookEntity?> GetById(Guid id)
        {
            var book = await _db.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }
    }
}

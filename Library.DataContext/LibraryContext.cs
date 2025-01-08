using Domain.Entities;
using Library.DataContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext
{
    public class LibraryContext : DbContext
    {
        public DbSet<AuthorEntity> Auhtors { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

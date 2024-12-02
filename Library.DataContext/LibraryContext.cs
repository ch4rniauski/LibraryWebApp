using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataContext
{
    public class LibraryContext : DbContext
    {
        public DbSet<AuthorEntity>? Auhtors { get; set; }
        public DbSet<BookEntity>? Books { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorEntity>().HasKey(a => a.Id);
            modelBuilder.Entity<AuthorEntity>(a =>
            {
                a.Property(a => a.Id).IsRequired();

                a.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(30);

                a.Property(a => a.SecondName)
                .IsRequired()
                .HasMaxLength(30);

                a.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(168);

                a.Property(a => a.BirthDate).IsRequired();

                a.HasMany(a => a.Books).WithOne(b => b.Author);
            });

            modelBuilder.Entity<BookEntity>().HasKey(b => b.Id);
            modelBuilder.Entity<BookEntity>(b =>
            {
                b.Property(b => b.Id).IsRequired();

                b.Property(b => b.ISBN).IsRequired();

                b.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);

                b.Property(b => b.Genre).HasMaxLength(89);

                b.Property(b => b.Description).HasMaxLength(100);

                b.Property(b => b.AuthorName).HasMaxLength(30);

                b.HasOne(b => b.Author).WithMany(a => a.Books);
            });
        }
    }
}

using Domain.Entities;
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
            modelBuilder.Entity<AuthorEntity>().HasKey(a => a.Id);
            modelBuilder.Entity<AuthorEntity>(a =>
            {
                a.Property(a => a.Id).IsRequired();

                a.Property(a => a.FirstName)
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

                b.Property(b => b.Description).HasMaxLength(250);

                b.Property(b => b.AuthorFirstName).HasMaxLength(30);

                b.Property(b => b.AuthorSecondName).HasMaxLength(30);

                b.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired(false);

                b.HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId)
                .IsRequired(false);
            });

            modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
            modelBuilder.Entity<UserEntity>(u =>
            {
                u.Property(u => u.Id).IsRequired();

                u.Property(u => u.Login).IsRequired();

                u.Property(u => u.Email).IsRequired();

                u.Property(u => u.PasswordHash).IsRequired();

                u.HasMany(u => u.Books)
                .WithOne(b => b.User);
            });
        }
    }
}

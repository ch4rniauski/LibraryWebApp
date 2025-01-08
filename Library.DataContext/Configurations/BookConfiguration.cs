using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataContext.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.Property(b => b.ISBN).IsRequired();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(89);

            builder.Property(b => b.Description).HasMaxLength(250);

            builder.Property(b => b.AuthorFirstName).HasMaxLength(30);

            builder.Property(b => b.AuthorSecondName).HasMaxLength(30);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired(false);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId)
                .IsRequired(false);

            builder.Property(b => b.ImageData)
                .HasColumnType("varbinary(max)")
                .HasMaxLength(int.MaxValue);
        }
    }
}

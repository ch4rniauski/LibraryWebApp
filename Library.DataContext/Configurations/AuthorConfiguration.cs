using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataContext.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired();

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.SecondName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(168);

            builder.Property(a => a.BirthDate).IsRequired();

            builder.HasMany(a => a.Books).WithOne(b => b.Author);
        }
    }
}

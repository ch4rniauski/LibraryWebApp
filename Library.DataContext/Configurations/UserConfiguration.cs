using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DataContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).IsRequired();

            builder.Property(u => u.Login).IsRequired();

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.IsAdmin).IsRequired();

            builder.HasMany(u => u.Books)
                .WithOne(b => b.User);
        }
    }
}

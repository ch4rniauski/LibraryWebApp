using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.DataContext
{
    public class AccountsContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
            modelBuilder.Entity<UserEntity>(u =>
            {
                u.Property(u => u.Id).IsRequired();
                u.Property(u => u.Login).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.Property(u => u.PasswordHash).IsRequired();
            });
        }
    }
}

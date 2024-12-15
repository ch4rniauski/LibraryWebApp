using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;
using Library.DataContext.Repositories;
using Library.DataContext.UnitsOfWork;
using LibraryAccounts.DataContext.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLibraryContext(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("LibraryDb"));
            });

            return services;
        }
    }
}

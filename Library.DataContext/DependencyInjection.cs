using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;
using Library.DataContext.Repositories;
using Library.DataContext.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLibraryContext(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWorkLibrary, UnitOfWork>();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("LibraryDb"));
            });

            return services;
        }
    }
}

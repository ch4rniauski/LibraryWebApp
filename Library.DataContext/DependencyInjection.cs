using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;
using Library.DataContext.Repositories;
using Library.DataContext.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=Library;User ID=Admin;Password=5432;TrustServerCertificate=true");
            });

            return services;
        }
    }
}

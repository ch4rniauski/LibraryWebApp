using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=GamesMarket;User ID=Admin;Password=5432;TrustServerCertificate=true");
            });

            return services;
        }
    }
}

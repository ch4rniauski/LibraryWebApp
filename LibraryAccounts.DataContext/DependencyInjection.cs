using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;
using Domain.JWT;
using LibraryAccounts.DataContext.Repositories;
using LibraryAccounts.DataContext.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.DataContext
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddAccountsContext(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUnitOfWorkAccounts, UnitOfWork>();

            services.AddDbContext<AccountsContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("AccountsDb"));
            });

            return services;
        }
    }
}

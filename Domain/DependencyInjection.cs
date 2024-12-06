using Domain.Abstractions.Records;
using Domain.JWT;
using Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<BookRecord>, BookValidator>();
            services.AddScoped<IValidator<UserRecord>, UserValidator>();

            return services;
        }

        static public IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddScoped<TokenProvider>();
            services.Configure<JWTSettings>(configurationManager.GetSection("JWTSettings"));

            return services;
        }
    }
}

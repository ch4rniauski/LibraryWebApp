using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Author>, AuthorValidator>();
            services.AddScoped<IValidator<Book>, BookValidator>();

            return services;
        }
    }
}

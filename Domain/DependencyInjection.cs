using Domain.Abstractions.Records;
using Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<BookRecord>, BookValidator>();

            return services;
        }
    }
}

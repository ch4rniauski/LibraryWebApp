using Domain.Abstractions.Records;
using Domain.Profiles;
using Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddDomainConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<CreateBookRecord>, BookValidator>();
            services.AddScoped<IValidator<RegisterUserRecord>, UserValidator>();

            return services;
        }
                
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AuthorProfile));

            return services;
        }
    }
}

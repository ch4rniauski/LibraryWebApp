using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Profiles.AuthorProfiles;
using Application.Profiles.BookProfiles;
using Application.Profiles.UserProfiles;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRecord>, UserValidator>();
            services.AddScoped<IValidator<CreateAuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<CreateBookRecord>, BookValidator>();

            services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BookEntityToGetBookResponse));
            services.AddAutoMapper(typeof(BookEntityToGetBookRecord));
            services.AddAutoMapper(typeof(CreateBookRecordToBookEntity));

            services.AddAutoMapper(typeof(RegisterUserRecordToLogInRequest));
            services.AddAutoMapper(typeof(RegisterUserRecordToUserEntity));
            services.AddAutoMapper(typeof(LogInRequestToRegisterUserRecord));
            services.AddAutoMapper(typeof(UserEntityToUserInfoResponse));

            services.AddAutoMapper(typeof(AuthorEntityToCreateAuthorRecord));
            services.AddAutoMapper(typeof(CreateAuthorRecordToAuthorEntity));
            services.AddAutoMapper(typeof(UpdateAuthorRecordToCreateAuthorRecord));

            return services;
        }
    }
}

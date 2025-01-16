using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Abstractions.UseCases.UserUseCases;
using Application.Profiles.AuthorProfiles;
using Application.Profiles.BookProfiles;
using Application.Profiles.UserProfiles;
using Application.Services;
using Application.UseCases.AuthenticationUserUseCases;
using Application.UseCases.AuthorUseCases;
using Application.UseCases.BookUseCases;
using Application.UseCases.UserUseCases;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            return services;
        }

        static public IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRecord>, UserValidator>();
            services.AddScoped<IValidator<CreateAuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<CreateBookRecord>, BookValidator>();

            return services;
        }

        static public IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        static public IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBookByISBNUseCase, GetBookByISBNUseCase>();
            services.AddScoped<IGetBooksByUserIdUseCase, GetBooksByUserIdUseCase>();
            services.AddScoped<IGetBooksWithParamsUseCase, GetBooksWithParamsUseCase>();
            services.AddScoped<IReturnBookUseCase, ReturnBookUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();

            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<ILogInUserUseCase, LogInUserUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IUpdateAccessTokenUseCase, UpdateAccessTokenUseCase>();

            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
            services.AddScoped<IDeleteAutorUseCase, DeleteAutorUseCase>();
            services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();

            services.AddScoped<IBorrowBookUseCase, BorrowBookUseCase>();
            services.AddScoped<IGetUserInfoUseCase, GetUserInfoUseCase>();

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
            services.AddAutoMapper(typeof(UpdateAuthorRecordToAuthorEntity));

            return services;
        }
    }
}

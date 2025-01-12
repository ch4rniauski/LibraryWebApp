using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Profiles.AuthorProfiles;
using Application.Profiles.BookProfiles;
using Application.Profiles.UserProfiles;
using Application.Services;
using Domain.Authorization.Handlers;
using Domain.Authorization.Requirements;
using Domain.JWT;
using Domain.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRecord>, UserValidator>();
            services.AddScoped<IValidator<CreateAuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<CreateBookRecord>, BookValidator>();

            services.AddScoped<TokenProvider>();

            services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        static public IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationHandler, AdminHandler>();

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWTSettings:SecretKey")!))
                    };

                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                        {
                            ctx.Request.Cookies.TryGetValue("accessToken", out string? accessToken);

                            if (accessToken is not null)
                                ctx.Token = accessToken;

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("AdminPolicy", policy =>
                {
                    policy.Requirements.Add(new AdminRequirement("admin"));
                });
            });

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

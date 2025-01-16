using Library.DataContext.Authorization.Handlers;
using Domain.Abstractions.JWT;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitsOfWork;
using Library.DataContext.JWT;
using Library.DataContext.Repositories;
using Library.DataContext.UnitsOfWork;
using LibraryAccounts.DataContext.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Library.DataContext.Authorization.Requirements;

namespace Library.DataContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLibraryContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LibraryDb"));
            });

            return services;
        }

        static public IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();

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
    }
}

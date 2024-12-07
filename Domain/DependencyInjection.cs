using Domain.Abstractions.Records;
using Domain.JWT;
using Domain.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new JWTSettings().SecretKey))
                    };
                });
            services.AddAuthorization();

            return services;
        }
    }
}

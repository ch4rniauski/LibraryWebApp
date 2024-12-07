﻿using Domain.Abstractions.Records;
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
        static public IServiceCollection AddDomainConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAuthorRecord>, AuthorValidator>();
            services.AddScoped<IValidator<CreateBookRecord>, BookValidator>();
            services.AddScoped<IValidator<UserRecord>, UserValidator>();
            services.AddScoped<TokenProvider>();

            return services;
        }

        static public IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configurationManager)
        {
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager.GetValue<string>("JWTSettings:SecretKey")!))
                    };
                });
            services.AddAuthorization();

            return services;
        }
    }
}

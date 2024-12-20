﻿using Domain.Abstractions.Records;
using Domain.Authorization.Handlers;
using Domain.Authorization.Requirements;
using Domain.JWT;
using Domain.Profiles;
using Domain.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IValidator<RegisterUserRecord>, UserValidator>();
            services.AddScoped<TokenProvider>();

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

            services.AddAuthorization( o =>
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
            services.AddAutoMapper(typeof(BookProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AuthorProfile));

            return services;
        }
    }
}

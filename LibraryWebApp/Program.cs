using Library.DataContext;
using Domain;
using Microsoft.AspNetCore.CookiePolicy;
using Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.WithOrigins("https://localhost:5173");
        p.AllowCredentials();
    });
});

builder.Services.AddLibraryContext(builder.Configuration);

builder.Services.AddDomainConfiguration();
builder.Services.AddJWTConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionsHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseCors();

app.MapControllers();

app.Run();

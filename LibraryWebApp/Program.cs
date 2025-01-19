using Library.DataContext;
using Microsoft.AspNetCore.CookiePolicy;
using Application;
using LibraryWebApp.Middleware;

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

builder.Services.AddJWTConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddUseCases();
builder.Services.AddValidators();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionsHandlerMiddleware>();

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

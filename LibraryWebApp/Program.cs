using Library.DataContext;
using Domain;
using Accounts.DataContext;

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
        p.WithOrigins("http://localhost:5173");
    });
});

builder.Services.AddLibraryContext(builder.Configuration);
builder.Services.AddAccountsContext(builder.Configuration);

builder.Services.AddDomainConfiguration();
builder.Services.AddJWTConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();

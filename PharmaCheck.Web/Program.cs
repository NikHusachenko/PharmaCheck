using Microsoft.EntityFrameworkCore;
using Npgsql;
using PharmaCheck.Domain.Category.GetCategories;
using PharmaCheck.EntityFramework;
using PharmaCheck.EntityFramework.Repositories.Factories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(config =>
{
    config.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
    });
});

services.AddSignalR();
services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetCategoriesRequestHandler).Assembly));

services.AddDbContext<ApplicationDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new NpgsqlException("Connection string is unavailable");
    }
    options.UseNpgsql(connectionString!);
});
services.AddScoped<IRepositoryFactory, RepositoryFactory>();

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
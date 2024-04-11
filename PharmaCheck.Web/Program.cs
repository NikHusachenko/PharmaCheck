using Microsoft.EntityFrameworkCore;
using PharmaCheck.EntityFramework;

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

services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));

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
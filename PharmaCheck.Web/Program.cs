using Microsoft.EntityFrameworkCore;
using PharmaCheck.Actors;
using PharmaCheck.EntityFramework;
using PharmaCheck.Web.Hubs;

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

services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("Server=127.0.0.1;Host=localhost;Database=PharmacyDb;Port=5432;username=postgres;password=admin"));

services.AddSingleton<ActorService>();
services.AddHostedService<ActorService>(provider => provider.GetRequiredService<ActorService>());

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

app.MapHub<MedicineHub>("/MedicineHub");

app.Run();
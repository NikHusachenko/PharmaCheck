using PharmaCheck.Actors;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddSignalR();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<RocklistDbContext>(options =>
{
    options.UseSqlite("Data Source=rocklist.db");
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
    options.LogTo(Console.WriteLine, LogLevel.Debug);
});

var library = Library.FromPath(@"D:\Libraries\Music\");
builder.Services.AddSingleton(library);

foreach (var artist in library.Artists) Console.WriteLine(artist);
foreach (var album in library.Albums) Console.WriteLine(album);
foreach (var track in library.Tracks) Console.WriteLine(track);

var app = builder.Build();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<RocklistDbContext>();
await db.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

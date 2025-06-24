using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rocklist.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var sqliteConnection = new SqliteConnection("Data Source=:memory:");
sqliteConnection.Open();
builder.Services.AddDbContext<RocklistDbContext>(options => {
	options.UseSqlite(sqliteConnection);
	options.EnableSensitiveDataLogging();
	options.EnableDetailedErrors();
	options.LogTo(Console.WriteLine, LogLevel.Information);
});

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

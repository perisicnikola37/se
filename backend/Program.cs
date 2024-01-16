using Microsoft.EntityFrameworkCore;
using Vega.classes;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
// var configuration = new ConfigurationBuilder()
//     .SetBasePath(builder.Environment.ContentRootPath)
//     .AddJsonFile("appsettings.json")
//     .Build();

// string connectionString = configuration.GetConnectionString("MyDbConnection");

// add DB context
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseMySql(
       "server=localhost;user=root;port=3306;Connect Timeout=5;database=first_database;password=06032004",
        new MySqlServerVersion(new Version(8, 0, 35))
    );
});

builder.Services.AddAuthentication();

// important for adding routes based on controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// auth
app.UseAuthentication();
app.UseAuthorization();

app.Run();


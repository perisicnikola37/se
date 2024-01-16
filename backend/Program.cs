using Microsoft.EntityFrameworkCore;
using Vega.classes;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o => { });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });
});

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
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

var app = builder.Build();
app.UseHttpLogging();
app.UseRateLimiter();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/", () => Results.Ok($"Hello {GetTicks()}"))
                           .RequireRateLimiting("fixed");

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

// cors
app.UseCors();

app.Run();


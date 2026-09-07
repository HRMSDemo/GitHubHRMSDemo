using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ScottTiger.Data;
using ScottTiger.Services;

var builder = WebApplication.CreateBuilder(args);

// ---- Data tier: EF Core over SQL Server -------------------------------
builder.Services.AddDbContext<ScottDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ScottConnection")));

// ---- Business tier ----------------------------------------------------
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// ---- Presentation plumbing -------------------------------------------
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow the static front end (any origin during the POC) to call the API.
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Create and seed the database on first run (demo convenience).
// For production, replace with EF migrations: dotnet ef database update.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScottDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();   // serves wwwroot/index.html at "/"
app.UseStaticFiles();
app.UseCors();
app.MapControllers();

app.Run();

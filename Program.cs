using Asp.Versioning;
using FullStackSession6.Repositories;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services;
using FullStackSession6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskFive.Middleware;
using TaskFour.Middleware;
using TaskSeven.Data;
using TaskSeven.Repositories;
using TaskSeven.Repositories.Interfaces;
using TaskSeven.Services;
using TaskSeven.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<SunsetMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

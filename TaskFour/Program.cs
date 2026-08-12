using FullStackSession6.Repositories;
using FullStackSession6.Repositories.Interfaces;
using FullStackSession6.Services;
using FullStackSession6.Services.Interfaces;
using TaskFour.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ITasksRepository, TasksRepository>();
builder.Services.AddSingleton<ITasksService, TasksService>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

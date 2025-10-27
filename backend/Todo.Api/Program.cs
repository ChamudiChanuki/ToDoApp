using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Services.Interfaces;
using Todo.Api.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Connection string comes from docker-compose env: ConnectionStrings__Default
var cs = builder.Configuration.GetConnectionString("Default")
         ?? builder.Configuration["ConnectionStrings:Default"];

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow React at http://localhost:3000
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.UseCors();
app.MapControllers();
app.Run();
public partial class Program { }
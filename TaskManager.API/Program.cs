using TaskManager.API.Controllers;
using TaskManager.Domain.Entities.ConfigurationModels;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Services.UserService;
using TaskManger.Infra.DataAccess;

var builder = WebApplication.CreateBuilder(args); 
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DefaultSettings>(builder.Configuration.GetSection("DefaultMongoDbSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.MapUserControllers();

app.Run();

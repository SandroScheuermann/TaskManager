using TaskManager.API.Controllers;
using TaskManager.API.DependencyInjection;
using TaskManager.Domain.Entities.ConfigurationModels;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Services.AssignmentService;
using TaskManager.Domain.Services.ProjectService;
using TaskManager.Domain.Services.UserService;
using TaskManger.Infra.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DefaultSettings>(builder.Configuration.GetSection("DefaultMongoDbSettings"));

builder.InjectDependencies();  

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserControllers();

app.Run();

using FluentValidation;
using TaskManager.API.Controllers;
using TaskManager.API.DependencyInjection;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Application.Validation.AssignmentValidations;

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

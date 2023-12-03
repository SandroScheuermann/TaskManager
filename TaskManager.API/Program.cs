using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using TaskManager.API.DependencyInjection;
using TaskManager.API.Mappings;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Application.Handlers.Assignments;
using TaskManager.Application.Handlers.Projects;
using TaskManager.Application.Handlers.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddSwaggerGen(config =>
{ 
    config.MapType<ObjectId>(() => new OpenApiSchema { Type = "string" });
});

builder.Services.Configure<DefaultSettings>(builder.Configuration.GetSection("DefaultMongoDbSettings"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(InsertUserHandler))); 

builder.InjectDependencies();
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.MapUserEndpoints();
app.MapProjectEndpoints();
app.MapAssignmentEndpoints();

app.Run();

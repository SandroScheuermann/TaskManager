using FluentValidation;
using TaskManager.Application.Services.AssignmentServices;
using TaskManager.Application.Validation.AssignmentValidations;
using TaskManager.Domain.Repositories;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class AssignmentDependencies
    {
        public static void InjectAssignmentDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddScoped<IAssignmentService, AssignmentService>(); 

            builder.Services.AddValidatorsFromAssemblyContaining<InsertAssignmentRequestValidator>(); 
        }
    }
}

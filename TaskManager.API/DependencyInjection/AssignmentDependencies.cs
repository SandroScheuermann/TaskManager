using FluentValidation;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Repositories.Assignments;
using TaskManger.Infra.Repositories.Assignments;

namespace TaskManager.API.DependencyInjection
{
    public static class AssignmentDependencies
    {
        public static void InjectAssignmentDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddScoped<IAssignmentLogRepository, AssignmentLogRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<InsertAssignmentCommandValidator>(); 
        }
    }
}

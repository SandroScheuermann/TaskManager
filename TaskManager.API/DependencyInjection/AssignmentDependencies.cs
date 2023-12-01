using FluentValidation;
using TaskManager.Application.Validators.Assignments;
using TaskManager.Domain.Repositories;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class AssignmentDependencies
    {
        public static void InjectAssignmentDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<InsertAssignmentRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateAssignmentRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GetAssignmentByIdRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DeleteAssignmentRequestValidator>();
        }
    }
}

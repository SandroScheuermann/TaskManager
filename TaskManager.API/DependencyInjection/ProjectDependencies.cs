using FluentValidation;
using TaskManager.Application.Validators.Projects;
using TaskManager.Application.Validators.Users;
using TaskManager.Domain.Repositories.Projects;
using TaskManger.Infra.Repositories.Projects;

namespace TaskManager.API.DependencyInjection
{
    public static class ProjectDependencies
    {
        public static void InjectProjectDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<InsertProjectCommandValidator>(); 
        } 
    }
}

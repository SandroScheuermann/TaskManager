using TaskManager.Domain.Repositories;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class ProjectDependencies
    {
        public static void InjectProjectDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>(); 
        } 
    }
}

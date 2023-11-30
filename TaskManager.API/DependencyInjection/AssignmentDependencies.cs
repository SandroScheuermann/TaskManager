using TaskManager.Domain.Repositories;
using TaskManager.Domain.Services.AssignmentService;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class AssignmentDependencies
    {
        public static void InjectAssignmentDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddScoped<IAssignmentService, AssignmentService>();
        } 
    }
}

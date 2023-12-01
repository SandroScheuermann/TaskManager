using Microsoft.AspNetCore.Hosting;

namespace TaskManager.API.DependencyInjection
{
    public static class GeneralDependencies
    {
        public static void InjectDependencies(this WebApplicationBuilder builder)
        {
            builder.InjectUserDependencies();
            builder.InjectProjectDependencies();
            builder.InjectAssignmentDependencies(); 
        }
    }
}

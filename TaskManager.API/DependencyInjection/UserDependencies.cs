using TaskManager.Domain.Repositories;
using TaskManager.Domain.Services.UserService;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class UserDependencies
    {
        public static void InjectUserDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>(); 
        } 
    }
}

using FluentValidation;
using TaskManager.Application.Validators.Projects;
using TaskManager.Domain.Repositories.Users;
using TaskManger.Infra.Repositories.Users;

namespace TaskManager.API.DependencyInjection
{
    public static class UserDependencies
    {
        public static void InjectUserDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>(); 

            builder.Services.AddValidatorsFromAssemblyContaining<InsertUserCommandValidator>();

        } 
    }
}

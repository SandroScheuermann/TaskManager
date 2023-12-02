using FluentValidation;
using TaskManager.Application.Validators.Projects;
using TaskManager.Domain.Repositories;
using TaskManger.Infra.DataAccess;

namespace TaskManager.API.DependencyInjection
{
    public static class UserDependencies
    {
        public static void InjectUserDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>(); 

            builder.Services.AddValidatorsFromAssemblyContaining<InsertUserRequestValidator>();

        } 
    }
}

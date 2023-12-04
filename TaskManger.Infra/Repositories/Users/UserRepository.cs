using Microsoft.Extensions.Options;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Users;
using TaskManager.Domain.Repositories.Users;
using TaskManger.Infra.Repositories.Shared;

namespace TaskManger.Infra.Repositories.Users
{
    public class UserRepository(IOptions<DefaultSettings> settings) : MongoRepository<User>(settings), IUserRepository
    {  
    }
}

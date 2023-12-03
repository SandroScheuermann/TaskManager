using Microsoft.Extensions.Options;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Users;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
{
    public class UserRepository(IOptions<DefaultSettings> settings) : MongoRepository<User>(settings), IUserRepository
    {
    }
}

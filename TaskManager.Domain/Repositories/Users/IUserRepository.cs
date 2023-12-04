using TaskManager.Domain.Entities.Users;
using TaskManager.Domain.Repositories.Shared;

namespace TaskManager.Domain.Repositories.Users
{
    public interface IUserRepository : IMongoRepository<User>
    {
    }
}

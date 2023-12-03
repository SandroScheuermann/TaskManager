using TaskManager.Domain.Entities.Users;

namespace TaskManager.Domain.Repositories
{
    public interface IUserRepository : IMongoRepository<User>
    {
    }
}

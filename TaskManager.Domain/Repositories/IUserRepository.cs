using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface IUserRepository : IMongoRepository<User>
    {
    }
}

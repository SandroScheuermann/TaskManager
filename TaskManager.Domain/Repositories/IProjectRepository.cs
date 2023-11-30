using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface IProjectRepository : IMongoRepository<Project>
    {
    }
}

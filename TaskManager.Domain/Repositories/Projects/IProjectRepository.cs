using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Repositories.Shared;

namespace TaskManager.Domain.Repositories.Projects
{
    public interface IProjectRepository : IMongoRepository<Project>
    {
        public Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);

    }
}

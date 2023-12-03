using TaskManager.Domain.Entities.Projects;

namespace TaskManager.Domain.Repositories
{
    public interface IProjectRepository : IMongoRepository<Project>
    {
        public Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);

    }
}

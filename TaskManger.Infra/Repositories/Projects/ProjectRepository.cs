using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Repositories.Projects;
using TaskManger.Infra.Repositories.Shared;

namespace TaskManger.Infra.Repositories.Projects
{
    public class ProjectRepository(IOptions<DefaultSettings> settings) : MongoRepository<Project>(settings), IProjectRepository
    {
        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId)
        {
            var getByIdFilter = Builders<Project>.Filter.Eq(entity => entity.UserId, userId);

            var cursor = await Collection.FindAsync(getByIdFilter);

            var projects = await cursor.ToListAsync();

            return projects;
        }
    }
}

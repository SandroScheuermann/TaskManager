using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
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

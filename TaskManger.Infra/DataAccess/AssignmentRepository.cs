using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Muscler.Infra.DataAccess.Shared;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManger.Infra.DataAccess
{
    public class AssignmentRepository(IOptions<DefaultSettings> settings) : MongoRepository<Assignment>(settings), IAssignmentRepository
    {
        public async Task<IEnumerable<Assignment>> GetPendingAssignmentsByProjectId(string projectId)
        {
            var filter = Builders<Assignment>.Filter.Eq(a => a.ProjectId, projectId)
                        & Builders<Assignment>.Filter.Ne(a => a.Status, AssignmentStatusEnum.Done);

            var cursor = await Collection.FindAsync(filter);
            var assignments = await cursor.ToListAsync();

            return assignments;
        }
    }
}

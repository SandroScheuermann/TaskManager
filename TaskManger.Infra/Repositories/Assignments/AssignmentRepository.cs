using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManger.Infra.Repositories.Shared;

namespace TaskManger.Infra.Repositories.Assignments
{
    public class AssignmentRepository(IOptions<DefaultSettings> settings) : MongoRepository<Assignment>(settings), IAssignmentRepository
    {
        public async Task<long> GetAssignmentsCountByProjectId(string projectId)
        {
            var filter = Builders<Assignment>.Filter.Eq(a => a.ProjectId, projectId);

            return await Collection.CountDocumentsAsync(filter);
        }

        public async Task<IEnumerable<Assignment>> GetPendingAssignmentsByProjectId(string projectId)
        {
            var filter = Builders<Assignment>.Filter.Eq(a => a.ProjectId, projectId)
                        & Builders<Assignment>.Filter.Ne(a => a.Status, AssignmentStatusEnum.Done);

            var cursor = await Collection.FindAsync(filter);
            var assignments = await cursor.ToListAsync();

            return assignments;
        }

        public async Task<UpdateResult> AddAssignmentComment(string assignmentId, Comment comment)
        {
            var filter = Builders<Assignment>.Filter.Eq(doc => doc.Id, assignmentId);
            var update = Builders<Assignment>.Update.Push(doc => doc.Comments, comment);

            return await Collection.UpdateOneAsync(filter, update);
        } 
    }
}

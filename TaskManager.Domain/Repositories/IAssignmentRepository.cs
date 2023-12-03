using MongoDB.Driver;
using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Domain.Repositories
{
    public interface IAssignmentRepository : IMongoRepository<Assignment>
    {
        public Task<IEnumerable<Assignment>> GetPendingAssignmentsByProjectId(string projectId);
        public Task<long> GetAssignmentsCountByProjectId(string projectId);
        public Task<UpdateResult> AddAssignmentComment(string assignmentId, Comment comment);
    }
}

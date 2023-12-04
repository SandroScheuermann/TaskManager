using MongoDB.Driver;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Shared;

namespace TaskManager.Domain.Repositories.Assignments
{
    public interface IAssignmentRepository : IMongoRepository<Assignment>
    {
        public Task<IEnumerable<Assignment>> GetPendingAssignmentsByProjectId(string projectId);
        public Task<long> GetAssignmentsCountByProjectId(string projectId);
        public Task<UpdateResult> AddAssignmentComment(string assignmentId, Comment comment);  
        public Task<IEnumerable<Assignment>> GetAssignmentsByProjectId(string userId); 
    }
}

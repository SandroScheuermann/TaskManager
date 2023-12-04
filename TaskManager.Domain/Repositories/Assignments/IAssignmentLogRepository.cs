using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Shared;

namespace TaskManager.Domain.Repositories.Assignments
{
    public interface IAssignmentLogRepository : IMongoRepository<AssignmentLog>
    {
        public Task<IEnumerable<AssignmentLog>> GetCompletedAssignmentsByUser(string userId, int numberOfDaysAgo = 0);
    }
}

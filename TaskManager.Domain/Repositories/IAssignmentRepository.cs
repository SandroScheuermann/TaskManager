using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface IAssignmentRepository : IMongoRepository<Assignment>
    {
        public Task<IEnumerable<Assignment>> GetPendingAssignmentsByProjectId(string projectId);
    }
}

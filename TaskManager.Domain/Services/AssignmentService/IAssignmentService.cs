using MongoDB.Driver;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services.AssignmentService
{
    public interface IAssignmentService
    {
        Task<Assignment> CreateAssignmentAsync(Assignment assignment);
        Task<Assignment> GetAssignmentByIdAsync(string id);
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<ReplaceOneResult> UpdateAssignmentAsync(Assignment assignment);
        Task<DeleteResult> DeleteAssignmentAsync(string id); 
    }
}

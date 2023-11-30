using MongoDB.Driver;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Domain.Services.AssignmentService
{
    public class AssignmentService(IAssignmentRepository assignmentRepository) : IAssignmentService
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
        {
            await AssignmentRepository.InsertAsync(assignment);

            return assignment;
        }

        public async Task<DeleteResult> DeleteAssignmentAsync(string id)
        {
            return await AssignmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await AssignmentRepository.GetAllAsync();
        }

        public async Task<Assignment> GetAssignmentByIdAsync(string id)
        {
            return await AssignmentRepository.GetByIdAsync(id);
        }

        public async Task<ReplaceOneResult> UpdateAssignmentAsync(Assignment assignment)
        {
            return await AssignmentRepository.UpdateAsync(assignment);
        }
    }
}

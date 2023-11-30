using MongoDB.Driver;
using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Validation.ErrorHandling;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services.AssignmentServicess
{
    public interface IAssignmentService
    {
        Task<Result<InsertAssignmentResponse, Error>> CreateAssignmentAsync(InsertAssignmentRequest assignmentRequest);
        Task<Assignment> GetAssignmentByIdAsync(string id);
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<ReplaceOneResult> UpdateAssignmentAsync(UpdateAssignmentRequest assignment);
        Task<DeleteResult> DeleteAssignmentAsync(string id);
    }
}

using FluentValidation;
using MongoDB.Driver;
using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Services.ProjectService;
using TaskManager.Application.Validation.ErrorHandling;
using TaskManager.Application.Validation.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services.AssignmentService
{
    public class AssignmentService(IAssignmentRepository assignmentRepository, IProjectService projectService, IValidator<InsertAssignmentRequest> assignmentValidator) : IAssignmentService
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectService ProjectService { get; set; } = projectService;
        public IValidator<InsertAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;  

        public async Task<Result<InsertAssignmentResponse, Error>> CreateAssignmentAsync(InsertAssignmentRequest assignmentRequest)
        {
            var validationResult = await AssignmentValidator.ValidateAsync(assignmentRequest);

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            var projectExist = await ProjectService.CheckIfProjectExists(assignmentRequest.ProjectId);

            if (!projectExist)
            {
                return new ProjectDoesntExistError();
            }

            var assignment = new Assignment()
            {
                Id = string.Empty,
                Description = assignmentRequest.Description,
                ExpireDate = assignmentRequest.ExpireDate,
                ProjectId = assignmentRequest.ProjectId,
                Title = assignmentRequest.Title,
                Status = assignmentRequest.Status,
            };

            await AssignmentRepository.InsertAsync(assignment);

            var response = new InsertAssignmentResponse()
            { 
                Title = assignment.Title
            };

            return response;
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

        public async Task<ReplaceOneResult> UpdateAssignmentAsync(UpdateAssignmentRequest assignment)
        { 
            return await AssignmentRepository.UpdateAsync(null);
        }
    }
}

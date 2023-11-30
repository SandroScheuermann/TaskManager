using FluentValidation;
using MongoDB.Driver;
using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Services.ProjectService;
using TaskManager.Application.Validation.ErrorHandling;
using TaskManager.Application.Validation.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services.AssignmentServicess
{
    public class AssignmentService(IAssignmentRepository assignmentRepository, IProjectService projectService, IValidator<InsertAssignmentRequest> assignmentValidator) : IAssignmentService
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectService ProjectService { get; set; } = projectService;
        public IValidator<InsertAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public async Task<Result<InsertAssignmentResponse, Error>> CreateAssignmentAsync(InsertAssignmentRequest assignmentRequest)
        {
            return await Task.Run(() => ValidateAssignmentRequest(assignmentRequest)
                                        .Bind(CheckIfProjectExists)
                                        .Bind(CreateAndInsertAssignment));
        }

        private Result<InsertAssignmentRequest, Error> ValidateAssignmentRequest(InsertAssignmentRequest request)
        {
            var validationResult = AssignmentValidator.Validate(request); // Supondo que essa validação é síncrona
            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }

        private Result<InsertAssignmentRequest, Error> CheckIfProjectExists(InsertAssignmentRequest request)
        {
            var projectExist = ProjectService.CheckIfProjectExists(request.ProjectId); // Supondo que essa verificação é síncrona
            if (!projectExist)
            {
                return new ProjectDoesntExistError();
            }

            return request;
        }

        private Result<InsertAssignmentResponse, Error> CreateAndInsertAssignment(InsertAssignmentRequest request)
        {
            var assignment = new Assignment()
            {
                // ... inicialização do assignment
            };

            AssignmentRepository.Insert(assignment); // Supondo que essa inserção é síncrona

            var response = new InsertAssignmentResponse()
            {
                Title = assignment.Title
            };

            return response;
        }







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

using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignments
{
    public class InsertAssignmentHandler(IAssignmentRepository assignmentRepository, IProjectRepository projectRepository, IValidator<CreateAssignmentRequest> assignmentValidator)
        : IRequestHandler<InsertAssignmentCommand, Result<InsertAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IValidator<CreateAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator; 

        public Task<Result<InsertAssignmentResponse, Error>> Handle(InsertAssignmentCommand command, CancellationToken cancellationToken)
        { 
             var response = ValidateRequest(command.Request)
                .Bind(CheckIfProjectExists)
                .Bind(CreateAndInsertAssignment);

            return Task.FromResult(response);
        }

        private Result<CreateAssignmentRequest, Error> ValidateRequest(CreateAssignmentRequest request)
        {
            var validationResult = AssignmentValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<CreateAssignmentRequest, Error> CheckIfProjectExists(CreateAssignmentRequest request)
        {
            var projectExist = ProjectRepository.CheckIfExistsById(request.ProjectId).Result;

            if (!projectExist)
            {
                return new ProjectDoesntExistError();
            }

            return request;
        }
        private Result<InsertAssignmentResponse, Error> CreateAndInsertAssignment(CreateAssignmentRequest request)
        {
            var assignment = new Assignment()
            {
                Id = string.Empty,
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                ExpireDate = request.ExpireDate,
                Status = request.Status,
                Priority = request.Priority,
            };

            AssignmentRepository.InsertAsync(assignment);

            var response = new InsertAssignmentResponse()
            {
                Title = assignment.Title
            };

            return response;
        }
    }
}

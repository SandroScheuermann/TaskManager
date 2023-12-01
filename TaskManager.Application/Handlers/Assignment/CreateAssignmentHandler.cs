using FluentValidation;
using MediatR;
using TaskManager.Application.Commands;
using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Services.ProjectService;
using TaskManager.Application.Validation.Errors;
using TaskManager.Application.Validation.ResultHandling;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Assignment
{
    public class CreateAssignmentHandler(IAssignmentRepository assignmentRepository, IProjectService projectService, IValidator<CreateAssignmentRequest> assignmentValidator) : IRequestHandler<CreateAssignmentCommand, Result<InsertAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectService ProjectService { get; set; } = projectService;
        public IValidator<CreateAssignmentRequest> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<InsertAssignmentResponse, Error>> Handle(CreateAssignmentCommand command, CancellationToken cancellationToken)
        {
            return Task.Run(() => ValidateAssignmentRequest(command.Request)
            .Bind(CheckIfProjectExists)
            .Bind(CreateAndInsertAssignment));
        }

        private Result<CreateAssignmentRequest, Error> ValidateAssignmentRequest(CreateAssignmentRequest request)
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
            var projectExist = ProjectService.CheckIfProjectExists(request.ProjectId).Result;

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
                Status = request.Status,
                ExpireDate = request.ExpireDate,
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

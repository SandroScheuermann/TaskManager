using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;

namespace TaskManager.Application.Handlers.Assignments
{
    public class InsertAssignmentHandler(
        IAssignmentRepository assignmentRepository, 
        IProjectRepository projectRepository, 
        IValidator<InsertAssignmentCommand> assignmentValidator) : 
        IRequestHandler<InsertAssignmentCommand, Result<InsertAssignmentResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IValidator<InsertAssignmentCommand> AssignmentValidator { get; set; } = assignmentValidator;

        public Task<Result<InsertAssignmentResponse, Error>> Handle(InsertAssignmentCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
               .Bind(CheckProjectExistance)
               .Bind(CheckProjectAssignmentsCount)
               .Bind(CreateAndInsertAssignment);

            return Task.FromResult(response);
        }

        private Result<InsertAssignmentCommand, Error> ValidateRequest(InsertAssignmentCommand command)
        {
            var validationResult = AssignmentValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<InsertAssignmentCommand, Error> CheckProjectExistance(InsertAssignmentCommand command)
        {
            var projectExist = ProjectRepository.CheckExistanceById(command.Request.ProjectId).Result;

            if (!projectExist)
            {
                return new ProjectNotFoundError();
            }

            return command;
        }
        private Result<InsertAssignmentCommand, Error> CheckProjectAssignmentsCount(InsertAssignmentCommand command)
        {
            var linkedAssignmentsCount = AssignmentRepository.GetAssignmentsCountByProjectId(command.Request.ProjectId).Result;

            if (linkedAssignmentsCount >= 20)
            {
                return new MaximumNumberOfAssignmentsError();
            }

            return command;
        }
        private Result<InsertAssignmentResponse, Error> CreateAndInsertAssignment(InsertAssignmentCommand command)
        {
            var assignment = new Assignment()
            {
                Id = string.Empty,
                ProjectId = command.Request.ProjectId,
                Title = command.Request.Title,
                Description = command.Request.Description,
                ExpirationDate = command.Request.ExpirationDate,
                Status = command.Request.Status,
                Priority = command.Request.Priority,
            };

            AssignmentRepository.InsertAsync(assignment);

            var response = new InsertAssignmentResponse()
            {
                Id = assignment.Id
            };

            return response;
        }
    }
}

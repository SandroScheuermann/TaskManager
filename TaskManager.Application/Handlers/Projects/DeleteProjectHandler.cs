using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Responses.Projects;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;

namespace TaskManager.Application.Handlers.Projects
{
    public class DeleteProjectHandler(IProjectRepository projectRepository, IAssignmentRepository assignmentRepository, IValidator<DeleteProjectCommand> assignmentValidator)
        : IRequestHandler<DeleteProjectCommand, Result<DeleteProjectResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<DeleteProjectCommand> ProjectValidator { get; set; } = assignmentValidator;  

        public Task<Result<DeleteProjectResponse, Error>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckProjectExistance)
                .Bind(CheckPendingAssignments)
                .Bind(DeleteProject);

            return Task.FromResult(response);
        }

        private Result<DeleteProjectCommand, Error> ValidateRequest(DeleteProjectCommand command)
        {
            var validationResult = ProjectValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<DeleteProjectCommand, Error> CheckProjectExistance(DeleteProjectCommand command)
        {
            var projectExists = ProjectRepository.CheckExistanceById(command.Id).Result;

            if (!projectExists)
            {
                return new ProjectDoesntExistError();
            }

            return command;
        }
        private Result<DeleteProjectCommand, Error> CheckPendingAssignments(DeleteProjectCommand command)
        {
            var pendingAssignments = AssignmentRepository.GetPendingAssignmentsByProjectId(command.Id).Result;

            if (pendingAssignments.Any())
            {
                return new PendingAssignmentsError(pendingAssignments);
            }

            return command;
        }
        private Result<DeleteProjectResponse, Error> DeleteProject(DeleteProjectCommand command)
        {
            var result = ProjectRepository.DeleteAsync(command.Id).Result;

            return result.DeletedCount > 0 ? new DeleteProjectResponse { } : new UnknownError("Failed to delete the informed project.");
        }
    }
}

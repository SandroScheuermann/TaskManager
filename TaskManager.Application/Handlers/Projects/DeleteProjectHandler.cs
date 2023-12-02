using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Projects
{
    public class DeleteProjectHandler(IProjectRepository projectRepository, IAssignmentRepository assignmentRepository, IValidator<DeleteProjectRequest> assignmentValidator)
        : IRequestHandler<DeleteProjectCommand, Result<DeleteProjectResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<DeleteProjectRequest> ProjectValidator { get; set; } = assignmentValidator;

        public Task<Result<DeleteProjectResponse, Error>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
                .Bind(CheckProjectExistance)
                .Bind(CheckPendingAssignments)
                .Bind(DeleteProject);

            return Task.FromResult(response);
        }

        private Result<DeleteProjectRequest, Error> ValidateRequest(DeleteProjectRequest request)
        {
            var validationResult = ProjectValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<DeleteProjectRequest, Error> CheckProjectExistance(DeleteProjectRequest request)
        {
            var projectExists = ProjectRepository.CheckExistanceById(request.Id).Result;

            if (!projectExists)
            {
                return new ProjectDoesntExistError();
            }

            return request;
        }
        private Result<DeleteProjectRequest, Error> CheckPendingAssignments(DeleteProjectRequest request)
        {
            var pendingAssignments = AssignmentRepository.GetPendingAssignmentsByProjectId(request.Id).Result;

            if (pendingAssignments.Any())
            {
                return new PendingAssignmentsError(pendingAssignments);
            }

            return request;
        }
        private Result<DeleteProjectResponse, Error> DeleteProject(DeleteProjectRequest request)
        {
            var result = ProjectRepository.DeleteAsync(request.Id).Result;

            return result.DeletedCount > 0 ? new DeleteProjectResponse { } : new UnknownError("Failed to delete the informed project.");
        }
    }
}

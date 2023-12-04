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
    public class GetProjectAssignmentsHandler(
        IAssignmentRepository assignmentRepository,
        IProjectRepository projectRepository,
        IValidator<GetProjectAssignmentsCommand> assignmentValidator) :
        IRequestHandler<GetProjectAssignmentsCommand, Result<GetProjectAssignmentsResponse, Error>>
    {
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IValidator<GetProjectAssignmentsCommand> ProjectValidator { get; set; } = assignmentValidator;

        public Task<Result<GetProjectAssignmentsResponse, Error>> Handle(GetProjectAssignmentsCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckProjectExistance)
                .Bind(GetAssignmentsByProjectId);

            return Task.FromResult(response);
        }

        private Result<GetProjectAssignmentsCommand, Error> ValidateRequest(GetProjectAssignmentsCommand command)
        {
            var validationResult = ProjectValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<GetProjectAssignmentsCommand, Error> CheckProjectExistance(GetProjectAssignmentsCommand command)
        {
            var userExists = ProjectRepository.CheckExistanceById(command.ProjectId).Result;

            if (!userExists)
            {
                return new ProjectNotFoundError();
            }

            return command;
        }
        private Result<GetProjectAssignmentsResponse, Error> GetAssignmentsByProjectId(GetProjectAssignmentsCommand command)
        {
            var assignments = AssignmentRepository.GetAssignmentsByProjectId(command.ProjectId).Result;

            return new GetProjectAssignmentsResponse { ProjectId = command.ProjectId, ProjectAssignments = assignments };
        }
    }
}

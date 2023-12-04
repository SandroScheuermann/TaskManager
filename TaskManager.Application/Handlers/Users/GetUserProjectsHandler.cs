using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories.Projects;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Application.Handlers.Users
{
    public class GetUserProjectsHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository, 
        IValidator<GetUserProjectsCommand> assignmentValidator) : 
        IRequestHandler<GetUserProjectsCommand, Result<GetUserProjectsResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IValidator<GetUserProjectsCommand> ProjectValidator { get; set; } = assignmentValidator;

        public Task<Result<GetUserProjectsResponse, Error>> Handle(GetUserProjectsCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckUserExistance)
                .Bind(GetProjectsByUserId);

            return Task.FromResult(response);
        }

        private Result<GetUserProjectsCommand, Error> ValidateRequest(GetUserProjectsCommand command)
        {
            var validationResult = ProjectValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<GetUserProjectsCommand, Error> CheckUserExistance(GetUserProjectsCommand command)
        {
            var userExists = UserRepository.CheckExistanceById(command.UserId).Result;

            if (!userExists)
            {
                return new UserNotFoundError();
            }

            return command;
        }
        private Result<GetUserProjectsResponse, Error> GetProjectsByUserId(GetUserProjectsCommand command)
        {
            var projects = ProjectRepository.GetProjectsByUserIdAsync(command.UserId).Result; 

            return new GetUserProjectsResponse { UserId = command.UserId, UserProjects = projects };
        }
    }
}

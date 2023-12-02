using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Users
{
    public class GetUserProjects(IProjectRepository assignmentRepository, IUserRepository userRepository, IValidator<GetUserProjectsRequest> assignmentValidator)
        : IRequestHandler<GetUserProjectsCommand, Result<GetUserProjectsResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = assignmentRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IValidator<GetUserProjectsRequest> ProjectValidator { get; set; } = assignmentValidator;

        public Task<Result<GetUserProjectsResponse, Error>> Handle(GetUserProjectsCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
                .Bind(CheckUserExistance)
                .Bind(GetProjectsByUserId);

            return Task.FromResult(response);
        }

        private Result<GetUserProjectsRequest, Error> ValidateRequest(GetUserProjectsRequest request)
        {
            var validationResult = ProjectValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<GetUserProjectsRequest, Error> CheckUserExistance(GetUserProjectsRequest request)
        {
            var userExists = UserRepository.CheckExistanceById(request.UserId).Result;

            if (!userExists)
            {
                return new UserDoesntExistError();
            }

            return request;
        }
        private Result<GetUserProjectsResponse, Error> GetProjectsByUserId(GetUserProjectsRequest request)
        {
            var projects = ProjectRepository.GetProjectsByUserIdAsync(request.UserId).Result;

            return new GetUserProjectsResponse { UserId = request.UserId, UserProjects = projects };
        }
    }
}

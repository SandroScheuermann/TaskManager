using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Projects;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Projects;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Application.Handlers.Projects
{
    public class InsertProjectHandler(IProjectRepository projectRepository, IUserRepository userRepository, IAssignmentRepository assignmentRepository, IValidator<InsertProjectRequest> projectValidator)
        : IRequestHandler<InsertProjectCommand, Result<InsertProjectResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;
        public IValidator<InsertProjectRequest> ProjectValidator { get; set; } = projectValidator;

        public Task<Result<InsertProjectResponse, Error>> Handle(InsertProjectCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command.Request)
               .Bind(CheckUserExistance)
               .Bind(CreateAndInsertProject);

            return Task.FromResult(response);
        }

        private Result<InsertProjectRequest, Error> ValidateRequest(InsertProjectRequest request)
        {
            var validationResult = ProjectValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        private Result<InsertProjectRequest, Error> CheckUserExistance(InsertProjectRequest request)
        {
            var userExist = UserRepository.CheckExistanceById(request.UserId).Result;

            if (!userExist)
            {
                return new UserNotFoundError();
            }

            return request;
        } 
        private Result<InsertProjectResponse, Error> CreateAndInsertProject(InsertProjectRequest request)
        {
            var project = new Project()
            {
                Id = string.Empty,
                UserId = request.UserId,
                ProjectName = request.ProjectName,
            };

            ProjectRepository.InsertAsync(project);

            var response = new InsertProjectResponse()
            {
                Id = project.Id
            };

            return response;
        }
    }
}

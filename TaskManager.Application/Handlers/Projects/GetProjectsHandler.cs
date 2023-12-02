using MediatR;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Responses.Projects;
using TaskManager.Application.ResultHandling;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Projects
{
    public class GetProjectsHandler(IProjectRepository assignmentRepository) : IRequestHandler<GetProjectsCommand, Result<GetProjectsResponse, Error>>
    {
        public IProjectRepository ProjectRepository { get; set; } = assignmentRepository; 

        public Task<Result<GetProjectsResponse, Error>> Handle(GetProjectsCommand command, CancellationToken cancellationToken)
        {
            var response = GetProjects();

            return Task.FromResult(response);
        }

        private Result<GetProjectsResponse, Error> GetProjects()
        {
            var assignments = ProjectRepository.GetAllAsync().Result;

            var response = new GetProjectsResponse()
            {
                Projects = assignments,
            };

            return response;
        }  
    }
}

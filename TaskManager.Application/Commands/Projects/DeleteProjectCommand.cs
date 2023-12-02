using MediatR;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Projects
{
    public class DeleteProjectCommand : IRequest<Result<DeleteProjectResponse, Error>>
    {
        public required DeleteProjectRequest Request { get; set; }
    }
}

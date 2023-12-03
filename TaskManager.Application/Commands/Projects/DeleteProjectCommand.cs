using MediatR;
using TaskManager.Application.Responses.Projects;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Projects
{
    public class DeleteProjectCommand : IRequest<Result<DeleteProjectResponse, Error>>
    {
        public required string Id { get; set; }
    }
}

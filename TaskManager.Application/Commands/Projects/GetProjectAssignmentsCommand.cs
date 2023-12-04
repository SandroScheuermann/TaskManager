using MediatR;
using TaskManager.Application.Responses.Projects;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Projects
{
    public class GetProjectAssignmentsCommand : IRequest<Result<GetProjectAssignmentsResponse, Error>>
    {
        public required string ProjectId { get; set; }
    }
}

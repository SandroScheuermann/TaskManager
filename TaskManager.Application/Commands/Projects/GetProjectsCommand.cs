using MediatR;
using TaskManager.Application.Responses.Projects;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Projects
{
    public class GetProjectsCommand : IRequest<Result<GetProjectsResponse, Error>>
    { 
    }
}

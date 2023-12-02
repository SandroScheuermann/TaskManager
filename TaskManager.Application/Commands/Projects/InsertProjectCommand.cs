using MediatR;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Projects
{
    public class InsertProjectCommand : IRequest<Result<InsertProjectResponse, Error>>
    {
        public required InsertProjectRequest Request { get; set; }  
    }
}

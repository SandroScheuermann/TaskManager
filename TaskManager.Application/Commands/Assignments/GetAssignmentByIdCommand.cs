using MediatR;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class GetAssignmentByIdCommand : IRequest<Result<GetAssignmentByIdResponse, Error>>
    {
        public required string Id { get; set; } 
    }
}

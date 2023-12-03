using MediatR;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class UpdateAssignmentCommand : IRequest<Result<UpdateAssignmentResponse, Error>>
    {
        public required string Id { get; set; }
        public required UpdateAssignmentRequest Request { get; set; } 
    }
}

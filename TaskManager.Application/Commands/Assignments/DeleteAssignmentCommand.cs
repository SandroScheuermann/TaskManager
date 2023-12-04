using MediatR;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class DeleteAssignmentCommand : IRequest<Result<DeleteAssignmentResponse, Error>>
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }  
    }
}

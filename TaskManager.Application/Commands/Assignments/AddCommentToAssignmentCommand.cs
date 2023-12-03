using MediatR;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class AddCommentToAssignmentCommand : IRequest<Result<AddCommentToAssignmentResponse, Error>>
    {
        public required string Id { get; set; }
        public required AddCommentToAssignmentRequest Request { get; set; } 
    }
}

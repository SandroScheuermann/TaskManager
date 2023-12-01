using MediatR;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class DeleteAssignmentCommand : IRequest<Result<DeleteAssignmentResponse, Error>>
    {
        public required DeleteAssignmentRequest Request { get; set; }
    }
}

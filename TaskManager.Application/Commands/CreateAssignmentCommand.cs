using MediatR;
using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Validation.ResultHandling;

namespace TaskManager.Application.Commands
{
    public class CreateAssignmentCommand : IRequest<Result<InsertAssignmentResponse, Error>>
    {
        public required CreateAssignmentRequest Request { get; set; }

    }
}

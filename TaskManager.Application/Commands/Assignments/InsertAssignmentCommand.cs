using MediatR;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class InsertAssignmentCommand : IRequest<Result<InsertAssignmentResponse, Error>>
    {
        public required InsertAssignmentRequest Request { get; set; }

    }
}

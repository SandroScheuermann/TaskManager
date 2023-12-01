using MediatR;
using TaskManager.Application.Responses.Assignments;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Assignments
{
    public class GetAssignmentsCommand : IRequest<Result<GetAssignmentsResponse, Error>>
    { 
    }
}

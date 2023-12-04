using MediatR;
using TaskManager.Application.ResultHandling;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Events.AssignmentLogs
{
    public class LogAssignmentUpdatesEvent : IRequest<Result<Task, Error>>
    {
        public required string UserId { get; set; }
        public required string AssignmentId { get; set; } 
        public required OperationTypeEnum OperationType { get; set; } 
    }
}

using MediatR;
using TaskManager.Application.Events.AssignmentLogs;
using TaskManager.Application.ResultHandling;
using TaskManager.Domain.Entities.Assignments;
using TaskManager.Domain.Repositories.Assignments;

namespace TaskManager.Application.Handlers.AssignmentLogs
{
    public class LogAssignmentUpdatesEventHandler(IAssignmentLogRepository assignmentLogRepository,
        IAssignmentRepository assignmentRepository)
        : IRequestHandler<LogAssignmentUpdatesEvent, Result<Task, Error>>
    {
        public IAssignmentLogRepository AssignmentLogRepository { get; set; } = assignmentLogRepository;
        public IAssignmentRepository AssignmentRepository { get; set; } = assignmentRepository;

        public Task<Result<Task, Error>> Handle(LogAssignmentUpdatesEvent logEvent, CancellationToken cancellationToken)
        {
            return CheckAndLogUpdatedProperties(logEvent); 
        }

        private async Task<Result<Task, Error>> CheckAndLogUpdatedProperties(LogAssignmentUpdatesEvent logEvent)
        {
            var updatedAssignment = AssignmentRepository.GetByIdAsync(logEvent.AssignmentId).Result;  

            var updateLog = new AssignmentLog
            {
                Id = null, 
                AssignmentId = logEvent.AssignmentId,
                UserId = logEvent.UserId,
                OperationType = logEvent.OperationType,
                AssignmentState = updatedAssignment,
                OperationDate = DateTime.Now
            };

            await AssignmentLogRepository.InsertAsync(updateLog);

            return Task.CompletedTask;
        }
    }
} 
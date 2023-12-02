using TaskManager.Domain.Entities;

namespace TaskManager.Application.ResultHandling.Errors
{
    public class PendingAssignmentsError(IEnumerable<Assignment> pendingAssignments) 
        : Error($"There is pending Assignments linked with this project : {string.Join(',', pendingAssignments)}")
    {
    }
}

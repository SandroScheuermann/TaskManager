using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Application.ResultHandling.Errors
{
    public class PendingAssignmentsError(IEnumerable<Assignment> pendingAssignments) 
        : Error($"Existem tarefas pendentes associadas a este projeto, remova ou conclua as seguintes tarefas : {string.Join(',', pendingAssignments)}")
    {
    }
}

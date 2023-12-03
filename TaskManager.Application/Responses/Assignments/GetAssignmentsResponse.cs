using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Application.Responses.Assignments
{
    public class GetAssignmentsResponse
    {
        public required IEnumerable<Assignment> Assignments { get; set; }
    }
}

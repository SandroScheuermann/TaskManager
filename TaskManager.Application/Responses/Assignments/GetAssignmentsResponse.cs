using TaskManager.Domain.Entities;

namespace TaskManager.Application.Responses.Assignments
{
    public class GetAssignmentsResponse
    {
        public required IEnumerable<Assignment> Assignments { get; set; }
    }
}

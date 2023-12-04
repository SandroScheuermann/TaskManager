using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Application.Responses.Projects
{
    public class GetProjectAssignmentsResponse
    {
        public required string ProjectId { get; set; }
        public required IEnumerable<Assignment>? ProjectAssignments { get; set; }
    }
}

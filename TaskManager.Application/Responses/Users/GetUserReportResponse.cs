using TaskManager.Domain.Entities.Assignments;

namespace TaskManager.Application.Responses.Users
{
    public class GetUserReportResponse
    {
        public required string UserId { get; set; }

        public required IEnumerable<Assignment> CompletedAssignments { get; set; }

        public int NumberOfAssignmentsCompleted { get => CompletedAssignments.Count(); }
    }
}

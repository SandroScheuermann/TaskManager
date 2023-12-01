using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Assignments
{
    public class UpdateAssignmentRequest
    {
        public required string Id { get; set; } 

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DateTime ExpireDate { get; set; }

        public required AssignmentStatusEnum Status { get; set; }

        public required AssignmentPriorityEnum Priority { get; set; }
    }
} 
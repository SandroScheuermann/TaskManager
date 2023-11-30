using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.AssignmentRequests
{
    public class InsertAssignmentRequest
    {
        public required string ProjectId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime ExpireDate { get; set; }
        public required AssignmentStatus Status { get; set; }  
    }
} 
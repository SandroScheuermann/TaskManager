namespace TaskManager.Application.Requests.Assignments
{
    public class AddCommentToAssignmentRequest
    {
        public required string UserId { get; set; }
        public required string Comment { get; set; }
    }
}
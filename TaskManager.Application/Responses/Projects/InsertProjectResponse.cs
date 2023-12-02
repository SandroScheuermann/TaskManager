namespace TaskManager.Application.Requests.Projects
{
    public class InsertProjectResponse
    {
        public required string UserId { get; set; }

        public IEnumerable<string>? AssignmentIds { get; set; }

    }
} 
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Project
{
    public class UpdateProjectRequest
    {
        public required string Id { get; set; }

        public string? Description { get; set; }


    }
} 
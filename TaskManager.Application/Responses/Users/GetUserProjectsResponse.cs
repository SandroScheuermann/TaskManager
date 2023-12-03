using TaskManager.Domain.Entities.Projects;

namespace TaskManager.Application.Responses.Users
{
    public class GetUserProjectsResponse
    {
        public required string UserId { get; set; }

        public IEnumerable<Project>? UserProjects { get; set; } = [];

    }
}

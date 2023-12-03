using TaskManager.Domain.Entities.Projects;

namespace TaskManager.Application.Responses.Projects
{
    public class GetProjectsResponse
    {
        public required IEnumerable<Project> Projects { get; set; }
    }
}

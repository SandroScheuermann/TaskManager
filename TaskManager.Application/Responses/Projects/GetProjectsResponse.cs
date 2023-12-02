using TaskManager.Domain.Entities;

namespace TaskManager.Application.Responses.Projects
{
    public class GetProjectsResponse
    {
        public required IEnumerable<Project> Projects { get; set; }
    }
}

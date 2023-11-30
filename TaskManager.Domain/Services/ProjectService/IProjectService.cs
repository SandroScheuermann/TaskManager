using MongoDB.Driver;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project user);
        Task<Project> GetProjectByIdAsync(string id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<ReplaceOneResult> UpdateProjectAsync(Project user);
        Task<DeleteResult> DeleteProjectAsync(string id); 
    }
}

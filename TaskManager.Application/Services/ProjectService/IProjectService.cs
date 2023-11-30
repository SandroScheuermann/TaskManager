using MongoDB.Driver;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project user);
        Task<Project> GetProjectByIdAsync(string id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<ReplaceOneResult> UpdateProjectAsync(Project user);
        Task<DeleteResult> DeleteProjectAsync(string id); 
        Task<bool> CheckIfProjectExists(string id);
    }
}

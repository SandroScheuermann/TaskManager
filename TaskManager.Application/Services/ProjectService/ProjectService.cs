using MongoDB.Driver;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services.ProjectService
{
    public class ProjectService(IProjectRepository projectRepository) : IProjectService
    {
        public IProjectRepository ProjectRepository { get; set; } = projectRepository;

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await ProjectRepository.InsertAsync(project);

            return project;
        }

        public async Task<DeleteResult> DeleteProjectAsync(string id)
        {
            return await ProjectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await ProjectRepository.GetAllAsync();
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await ProjectRepository.GetByIdAsync(id);
        }

        public async Task<ReplaceOneResult> UpdateProjectAsync(Project project)
        {
            return await ProjectRepository.UpdateAsync(project);
        }

        public async Task<bool> CheckIfProjectExists(string id)
        {
            return await ProjectRepository.CheckIfExistsById(id);
        }
    }
}

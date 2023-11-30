using TaskManager.API.Requests.ProjectRequests;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services.ProjectService;

namespace TaskManager.API.Controllers
{
    public static class ProjectController
    {
        public static void MapProjectControllers(this WebApplication app)
        {
            _ = app.MapPost("/projects", InsertProject);
            _ = app.MapGet("/projects", GetProjects);
            _ = app.MapGet("/projects/{id}", GetProjectById);
            _ = app.MapDelete("/projects/{id}", DeleteProject);
            _ = app.MapPut("/projects", UpdateProject);
        }

        private static async Task<IResult> InsertProject(InsertProjectRequest insertProjectRequest, IProjectService ProjectService)
        {
            var project = new Project { Id = "" , UserId = ""};

            //validar

            await ProjectService.CreateProjectAsync(project);

            return Results.Ok();
        }
        private static async Task<IResult> GetProjects(IProjectService ProjectService)
        {
            var projectsReturned = await ProjectService.GetAllProjectsAsync();

            return Results.Ok(projectsReturned);
        }
        private static async Task<IResult> GetProjectById(string id, IProjectService ProjectService)
        {
            var projectReturned = await ProjectService.GetProjectByIdAsync(id);

            return Results.Ok(projectReturned);
        }
        private static async Task<IResult> DeleteProject(string id, IProjectService ProjectService)
        {
            var deleteResponse = await ProjectService.DeleteProjectAsync(id);

            return deleteResponse.DeletedCount > 0 ? Results.Ok(deleteResponse.DeletedCount) : Results.NotFound(id);
        }
        private static async Task<IResult> UpdateProject(UpdateProjectRequest updateProjectRequest, IProjectService ProjectService)
        {
            var updatedProject = new Project { Id = updateProjectRequest.Id, UserId = updateProjectRequest.Id };

            var updateResponse = await ProjectService.UpdateProjectAsync(updatedProject);

            return updateResponse.MatchedCount > 0 ? Results.Ok(updateResponse.UpsertedId) : Results.NotFound();
        }
    }
}

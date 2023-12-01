using TaskManager.Application.Requests.Projects;

namespace TaskManager.API.Controllers
{
    public static class ProjectController
    {
        public static void MapProjectControllers(this WebApplication app)
        {
            var group = app.MapGroup("/projects");

            group.MapPost("/", InsertProject);
            group.MapGet("/", GetProjects);
            group.MapGet("/{id}", GetProjectById);
            group.MapDelete("/{id}", DeleteProject);
            group.MapPut("/", UpdateProject);
        }

        private static async Task<IResult> InsertProject(InsertProjectRequest insertProjectRequest)
        {

            return Results.Ok();
        }
        private static async Task<IResult> GetProjects()
        {

            return Results.Ok();
        }
        private static async Task<IResult> GetProjectById(string id)
        {

            return Results.Ok();
        }
        private static async Task<IResult> DeleteProject(string id)
        {

            return Results.Ok();
        }
        private static async Task<IResult> UpdateProject(UpdateProjectRequest updateProjectRequest)
        {
            return Results.Ok();
        }
    }
}

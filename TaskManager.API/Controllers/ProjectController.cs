﻿using System.Text.RegularExpressions;
using TaskManager.Application.Requests.ProjectRequests;
using TaskManager.Application.Services.ProjectService;
using TaskManager.Domain.Entities;

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

        private static async Task<IResult> InsertProject(InsertProjectRequest insertProjectRequest, IProjectService ProjectService)
        {
            await ProjectService.CreateProjectAsync(null);

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
            var updateResponse = await ProjectService.UpdateProjectAsync(null);

            return updateResponse.MatchedCount > 0 ? Results.Ok(updateResponse.UpsertedId) : Results.NotFound();
        }
    }
}

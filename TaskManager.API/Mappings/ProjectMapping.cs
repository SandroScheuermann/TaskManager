﻿using MediatR;
using TaskManager.Application.Commands.Projects;
using TaskManager.Application.Requests.Projects;
using TaskManager.Application.ResultHandling.Errors;

namespace TaskManager.API.Mappings
{
    public static class ProjectMapping
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/projects");

            group.MapPost("/", InsertProject);
            group.MapGet("/", GetProjects); 
            group.MapDelete("/{id}", DeleteProject);
            group.MapGet("/{id}/assignments", GetProjectAssignments);

        }

        private static async Task<IResult> InsertProject(InsertProjectRequest insertProjectRequest, IMediator mediator)
        {
            var insertCommand = new InsertProjectCommand { Request = insertProjectRequest };

            var response = await mediator.Send(insertCommand);

            return response.Match(
                    success => Results.Ok(success),
                    error => error switch
                    {
                        RequestValidationError => Results.BadRequest(error.Message),
                        AssignmentNotFoundError => Results.NotFound(error.Message), 
                        _ => Results.Problem(error.Message)
                    });
        }
        private static async Task<IResult> GetProjects(IMediator mediator)
        {
            var getCommand = new GetProjectsCommand();

            var response = await mediator.Send(getCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    _ => Results.Problem(error.Message)
                });
        } 
        private static async Task<IResult> DeleteProject(string id, IMediator mediator)
        {  
            var deleteCommand = new DeleteProjectCommand { Id = id };

            var response = await mediator.Send(deleteCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    ProjectNotFoundError => Results.NotFound(error.Message),
                    FailedToDeleteError => Results.Problem(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }


        private static async Task<IResult> GetProjectAssignments(string id, IMediator mediator)
        {
            var getCommand = new GetProjectAssignmentsCommand { ProjectId = id };

            var response = await mediator.Send(getCommand);

            return response.Match(
                    success => Results.Ok(success),
                    error => error switch
                    {
                        RequestValidationError => Results.BadRequest(error.Message),
                        UserNotFoundError => Results.NotFound(error.Message),
                        _ => Results.Problem(error.Message)
                    });
        }

    }
}

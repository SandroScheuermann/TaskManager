using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.ResultHandling.Errors;

namespace TaskManager.API.Mappings
{
    public static class UserMapping
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/users");

            group.MapPost("/", InsertUser);
            group.MapGet("/{id}/projects", GetUserProjects);

            group.MapGet("/{adminUserId}/{id}/assignments/report", GetUserAssignmentsReport);
            group.MapGet("/projects/report", GetAllUsersAssignmentsReport);
        }

        private static async Task<IResult> InsertUser(InsertUserRequest insertUserRequest, IMediator mediator)
        {
            var insertCommand = new InsertUserCommand { Request = insertUserRequest };

            var response = await mediator.Send(insertCommand);

            return response.Match(
                    success => Results.Ok(success),
                    error => error switch
                    {
                        RequestValidationError => Results.BadRequest(error.Message),
                        _ => Results.Problem(error.Message)
                    });
        }
        private static async Task<IResult> GetUserProjects(string id, IMediator mediator)
        {
            var getCommand = new GetUserProjectsCommand { UserId = id };

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
        private static async Task<IResult> GetUserAssignmentsReport(string adminUserId, string id, IMediator mediator)
        {
            var getCommand = new GetUserReportCommand { UserId = id, AdminUserId = adminUserId };

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
        private static async Task<IResult> GetAllUsersAssignmentsReport(string id, IMediator mediator)
        {
            var getCommand = new GetUserProjectsCommand { UserId = id };

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

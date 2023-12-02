using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.ResultHandling.Errors;

namespace TaskManager.API.Controllers
{
    public static class UserController
    {
        public static void MapUserControllers(this WebApplication app)
        {
            var group = app.MapGroup("/users");

            group.MapPost("/", InsertUser);
            group.MapGet("/", GetUsers);
            group.MapGet("/{id}", GetUserById);
            group.MapDelete("/{id}", DeleteUser);
            group.MapPut("/", UpdateUser);

            group.MapGet("/{id}/projects", GetUserProjects);

        }

        private static async Task<IResult> InsertUser()
        { 

            return Results.Ok();
        }
        private static async Task<IResult> GetUsers()
        {

            return Results.Ok();
        }
        private static async Task<IResult> GetUserById(string id)
        {

            return Results.Ok();
        }
        private static async Task<IResult> DeleteUser(string id)
        {

            return Results.Ok();
        }
        private static async Task<IResult> UpdateUser()
        {

            return Results.Ok();
        } 

        private static async Task<IResult> GetUserProjects(string id, IMediator mediator)
        {
            var request = new GetUserProjectsRequest { UserId = id };

            var getCommand = new GetUserProjectsCommand { Request = request };

            var response = await mediator.Send(getCommand);

            return response.Match(
                    success => Results.Ok(success),
                    error => error switch
                    {
                        RequestValidationError => Results.BadRequest(error.Message),
                        UserDoesntExistError => Results.NotFound(error.Message),
                        _ => Results.Problem(error.Message)
                    });
        }
    }
}

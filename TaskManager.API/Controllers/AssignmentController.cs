using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling.Errors;

namespace TaskManager.API.Controllers
{
    public static class AssignmentController
    {
        public static void MapAssignmentControllers(this WebApplication app)
        {
            var group = app.MapGroup("/assignment");

            group.MapPost("/", InsertAssignment);
            group.MapGet("/", GetAssignments);
            group.MapGet("/{id}", GetAssignmentById);
            group.MapDelete("/{id}", DeleteAssignment);
            group.MapPatch("/", UpdateAssignment);
        }

        private static async Task<IResult> InsertAssignment(CreateAssignmentRequest insertAssignmentRequest, IMediator Mediator)
        {
            var insertCommand = new InsertAssignmentCommand { Request = insertAssignmentRequest };

            var response = await Mediator.Send(insertCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    ProjectDoesntExistError => Results.NotFound(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> GetAssignments(IMediator Mediator)
        {
            var getCommand = new GetAssignmentsCommand();

            var response = await Mediator.Send(getCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> GetAssignmentById(string id, IMediator Mediator)
        {
            var getByIdRequest = new GetAssignmentByIdRequest { Id = id };

            var getByIdCommand = new GetAssignmentByIdCommand { Request = getByIdRequest };

            var response = await Mediator.Send(getByIdCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.Problem(error.Message),
                    AssignmentDoesntExistError => Results.NotFound(),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> DeleteAssignment(string id, IMediator Mediator)
        {
            var deleteRequest = new DeleteAssignmentRequest { Id = id };

            var deleteCommand = new DeleteAssignmentCommand { Request = deleteRequest };

            var response = await Mediator.Send(deleteCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    UnknownError => Results.Problem(error.Message),
                    AssignmentDoesntExistError => Results.NotFound(),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> UpdateAssignment(UpdateAssignmentRequest updateAssignmentRequest, IMediator Mediator)
        {
            var updateCommand = new UpdateAssignmentCommand { Request = updateAssignmentRequest };

            var response = await Mediator.Send(updateCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    UnknownError => Results.Problem(error.Message),
                    AssignmentDoesntExistError => Results.NotFound(),
                    _ => Results.Problem(error.Message)
                });
        }
    }
}

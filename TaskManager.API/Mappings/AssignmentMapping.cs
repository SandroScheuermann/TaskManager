using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling.Errors;

namespace TaskManager.API.Mappings
{
    public static class AssignmentMapping
    {
        public static void MapAssignmentEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/assignment");

            group.MapPost("/", InsertAssignment);
            group.MapGet("/", GetAssignments);
            group.MapGet("/{id}", GetAssignmentById);
            group.MapDelete("/{id}", DeleteAssignment);
            group.MapPatch("/{id}", UpdateAssignment);
            group.MapPatch("/{id}/comments", AddCommentToAssignment);
        }

        private static async Task<IResult> InsertAssignment(InsertAssignmentRequest insertAssignmentRequest, IMediator Mediator)
        {
            var insertCommand = new InsertAssignmentCommand { Request = insertAssignmentRequest };

            var response = await Mediator.Send(insertCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    ProjectDoesntExistError => Results.NotFound(error.Message),
                    MaximumNumberOfAssignmentsError => Results.BadRequest(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> GetAssignments(IMediator mediator)
        {
            var getCommand = new GetAssignmentsCommand();

            var response = await mediator.Send(getCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> GetAssignmentById(string id, IMediator mediator)
        {  
            var getByIdCommand = new GetAssignmentByIdCommand { Id = id };

            var response = await mediator.Send(getByIdCommand);

            return response.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.Problem(error.Message),
                    AssignmentDoesntExistError => Results.NotFound(),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> DeleteAssignment(string id, IMediator mediator)
        {  
            var deleteCommand = new DeleteAssignmentCommand { Id = id };

            var response = await mediator.Send(deleteCommand);

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
        private static async Task<IResult> UpdateAssignment(string id, UpdateAssignmentRequest updateAssignmentRequest, IMediator mediator)
        {
            var updateCommand = new UpdateAssignmentCommand { Request = updateAssignmentRequest, Id = id };

            var response = await mediator.Send(updateCommand);

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
        private static async Task<IResult> AddCommentToAssignment(string id, AddCommentToAssignmentRequest addCommentToAssignmentRequest, IMediator mediator)
        {
            var addCommentCommand = new AddCommentToAssignmentCommand { Request = addCommentToAssignmentRequest, Id = id };

            var response = await mediator.Send(addCommentCommand);

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

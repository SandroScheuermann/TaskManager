using MediatR;
using TaskManager.Application.Commands.Assignments;
using TaskManager.Application.Events.AssignmentLogs;
using TaskManager.Application.Requests.Assignments;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Enums;

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

        private static async Task<IResult> InsertAssignment(InsertAssignmentRequest insertAssignmentRequest, IMediator mediator)
        {
            var insertCommand = new InsertAssignmentCommand { Request = insertAssignmentRequest };

            var response = await mediator.Send(insertCommand);

            return response.Match(
            success =>
                {
                    mediator.Send(new LogAssignmentUpdatesEvent
                    {
                        AssignmentId = response.Value!.Id,
                        OperationType = OperationTypeEnum.Create,
                        UserId = insertAssignmentRequest.UserId,
                    });

                    return Results.Ok(success);
                },
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    ProjectNotFoundError => Results.NotFound(error.Message),
                    MaximumNumberOfAssignmentsError => Results.UnprocessableEntity(error.Message),
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
                    AssignmentNotFoundError => Results.NotFound(),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> DeleteAssignment(string id, string userId, IMediator mediator)
        {
            var deleteCommand = new DeleteAssignmentCommand { Id = id, UserId = userId };

            var response = await mediator.Send(deleteCommand);

            return response.Match(
                success =>
                {
                    mediator.Send(new LogAssignmentUpdatesEvent
                    {
                        AssignmentId = id,
                        OperationType = OperationTypeEnum.Delete,
                        UserId = userId,
                    });

                    return Results.Ok(success);
                },
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    AssignmentNotFoundError => Results.NotFound(error.Message),
                    UnknownError => Results.Problem(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> UpdateAssignment(string id, UpdateAssignmentRequest updateAssignmentRequest, IMediator mediator)
        {
            var updateCommand = new UpdateAssignmentCommand { Request = updateAssignmentRequest, Id = id };

            var response = await mediator.Send(updateCommand);

            return response.Match(
                success =>
                {
                    mediator.Send(new LogAssignmentUpdatesEvent
                    {
                        AssignmentId = id,
                        OperationType = OperationTypeEnum.Update,
                        UserId = updateAssignmentRequest.UserId
                    });

                    return Results.Ok(success);
                },
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    AssignmentNotFoundError => Results.NotFound(error.Message),
                    UnknownError => Results.Problem(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }
        private static async Task<IResult> AddCommentToAssignment(string id, AddCommentToAssignmentRequest addCommentToAssignmentRequest, IMediator mediator)
        {
            var addCommentCommand = new AddCommentToAssignmentCommand { Request = addCommentToAssignmentRequest, Id = id };

            var response = await mediator.Send(addCommentCommand);

            return response.Match(
                success =>
                {
                    mediator.Send(new LogAssignmentUpdatesEvent
                    {
                        AssignmentId = id,
                        OperationType = OperationTypeEnum.Comment,
                        UserId = addCommentToAssignmentRequest.UserId
                    });

                    return Results.Ok(success);
                },
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    AssignmentNotFoundError => Results.NotFound(error.Message),
                    UnknownError => Results.Problem(error.Message),
                    _ => Results.Problem(error.Message)
                });
        }
    }
}
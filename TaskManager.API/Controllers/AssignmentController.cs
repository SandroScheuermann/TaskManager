using TaskManager.Application.Requests.AssignmentRequests;
using TaskManager.Application.Services.AssignmentServices;
using TaskManager.Application.Validation.Errors;

namespace TaskManager.API.Controllers
{
    public static class AssignmentController
    {
        public static void MapAssignmentControllers(this WebApplication app)
        {
            _ = app.MapPost("/assignments", InsertAssignment);
            _ = app.MapGet("/assignments", GetAssignments);
            _ = app.MapGet("/assignments/{id}", GetAssignmentById);
            _ = app.MapDelete("/assignments/{id}", DeleteAssignment);
            _ = app.MapPut("/assignments", UpdateAssignment);
        }

        private static async Task<IResult> InsertAssignment(InsertAssignmentRequest insertAssignmentRequest, IAssignmentService AssignmentService)
        {
            var result = await AssignmentService.CreateAssignmentAsync(insertAssignmentRequest);

            return result.Match(
                success => Results.Ok(success),
                error => error switch
                {
                    RequestValidationError => Results.BadRequest(error.Message),
                    ProjectDoesntExistError => Results.BadRequest(error.Message),
                    _ => Results.Problem()
                });  
        }
        private static async Task<IResult> GetAssignments(IAssignmentService AssignmentService)
        {
            var assignmentsReturned = await AssignmentService.GetAllAssignmentsAsync();

            return Results.Ok(assignmentsReturned);
        }
        private static async Task<IResult> GetAssignmentById(string id, IAssignmentService AssignmentService)
        {
            var assignmentReturned = await AssignmentService.GetAssignmentByIdAsync(id);

            return Results.Ok(assignmentReturned);
        }
        private static async Task<IResult> DeleteAssignment(string id, IAssignmentService AssignmentService)
        {
            var deleteResponse = await AssignmentService.DeleteAssignmentAsync(id);

            return deleteResponse.DeletedCount > 0 ? Results.Ok(deleteResponse.DeletedCount) : Results.NotFound(id);
        }
        private static async Task<IResult> UpdateAssignment(UpdateAssignmentRequest updateAssignmentRequest, IAssignmentService AssignmentService)
        {
            //var updatedAssignment = new Assignment
            //{
            //    Id = "",
            //    Title = "",
            //    Description = "",
            //    ExpireDate = DateTime.Now,
            //    Status = AssignmentStatus.Done
            //};

            var updateResponse = await AssignmentService.UpdateAssignmentAsync(null);

            return updateResponse.MatchedCount > 0 ? Results.Ok(updateResponse.UpsertedId) : Results.NotFound();
        }
    }
}

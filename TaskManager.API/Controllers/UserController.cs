

using TaskManager.Application.Requests.UserRequests;

namespace TaskManager.API.Controllers
{
    public static class UserController
    {
        public static void MapUserControllers(this WebApplication app)
        {
            _ = app.MapPost("/users", InsertUser);
            _ = app.MapGet("/users", GetUsers);
            _ = app.MapGet("/users/{id}", GetUserById);
            _ = app.MapDelete("/users/{id}", DeleteUser);
            _ = app.MapPut("/users", UpdateUser);
        }

        private static async Task<IResult> InsertUser(InsertUserRequest insertUserRequest, IUserService UserService)
        {
            var user = new User { UserName = insertUserRequest.UserName, Id = "" };

            //validar

            await UserService.CreateUserAsync(user);

            return Results.Ok();
        }
        private static async Task<IResult> GetUsers(IUserService UserService)
        {
            var usersReturned = await UserService.GetAllUsersAsync();

            return Results.Ok(usersReturned);
        }
        private static async Task<IResult> GetUserById(string id, IUserService UserService)
        {
            var userReturned = await UserService.GetUserByIdAsync(id);

            return Results.Ok(userReturned);
        }
        private static async Task<IResult> DeleteUser(string id, IUserService UserService)
        {
            var deleteResponse = await UserService.DeleteUserAsync(id);

            return deleteResponse.DeletedCount > 0 ? Results.Ok(deleteResponse.DeletedCount) : Results.NotFound(id);
        }
        private static async Task<IResult> UpdateUser(UpdateUserRequest updateUserRequest, IUserService UserService)
        {
            var updatedUser = new User { Id = updateUserRequest.Id, UserName = updateUserRequest.UserName };

            var updateResponse = await UserService.UpdateUserAsync(updatedUser);

            return updateResponse.MatchedCount > 0 ? Results.Ok(updateResponse.UpsertedId) : Results.NotFound();
        }
    }
}

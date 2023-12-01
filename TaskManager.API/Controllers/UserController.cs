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
    }
}

namespace TaskManager.Application.Requests.Users
{
    public class UpdateUserRequest
    {
        public required string Id { get; set; } 

        public required string UserName { get; set; } 
    }
}

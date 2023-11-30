namespace TaskManager.API.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        public required string Id { get; set; } 

        public required string UserName { get; set; } 
    }
}

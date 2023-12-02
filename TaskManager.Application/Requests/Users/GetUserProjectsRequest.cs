using MongoDB.Bson;

namespace TaskManager.Application.Requests.Users
{
    public class GetUserProjectsRequest
    {
        public required string UserId { get; set; }
    }
}

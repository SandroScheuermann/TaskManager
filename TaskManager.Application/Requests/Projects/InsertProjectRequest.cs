using MongoDB.Bson;

namespace TaskManager.Application.Requests.Projects
{
    public class InsertProjectRequest
    {  
        public required string UserId { get; set; }

        public required string ProjectName { get; set; }
    }
} 
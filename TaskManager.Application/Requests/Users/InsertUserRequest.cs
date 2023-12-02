using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Users
{
    public class InsertUserRequest
    {
        public required string UserName { get; set; }

        public required UserRoleEnum UserRole { get; set; }
    }
}

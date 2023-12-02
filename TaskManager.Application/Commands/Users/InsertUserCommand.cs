using MediatR;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Users
{
    public class InsertUserCommand : IRequest<Result<InsertUserResponse, Error>>
    {
        public required InsertUserRequest Request { get; set; } 
    }
}

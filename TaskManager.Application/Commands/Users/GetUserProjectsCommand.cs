using MediatR;
using TaskManager.Application.Requests.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Users
{
    public class GetUserProjectsCommand : IRequest<Result<GetUserProjectsResponse, Error>>
    {
        public required GetUserProjectsRequest Request { get; set; }
    }
}

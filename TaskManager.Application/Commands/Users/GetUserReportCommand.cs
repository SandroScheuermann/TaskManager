using MediatR;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;

namespace TaskManager.Application.Commands.Users
{
    public class GetUserReportCommand : IRequest<Result<GetUserReportResponse, Error>>
    {
        public required string UserId { get; set; }

        public required string AdminUserId { get; set; }
    }
}

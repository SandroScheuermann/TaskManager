using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories.Assignments;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Application.Handlers.Users
{
    public class GetUserReport(
        IAssignmentLogRepository assignmentLogRepository,
        IUserRepository userRepository,
        IValidator<GetUserReportCommand> assignmentValidator) :
        IRequestHandler<GetUserReportCommand, Result<GetUserReportResponse, Error>>
    {
        public IAssignmentLogRepository AssignmentLogRepository { get; set; } = assignmentLogRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IValidator<GetUserReportCommand> ProjectValidator { get; set; } = assignmentValidator;

        public Task<Result<GetUserReportResponse, Error>> Handle(GetUserReportCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
                .Bind(CheckUserPermission)
                .Bind(GenerateUserReportInTheLastThirtyDays);

            return Task.FromResult(response);
        }

        private Result<GetUserReportCommand, Error> ValidateRequest(GetUserReportCommand command)
        {
            var validationResult = ProjectValidator.ValidateAsync(command).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return command;
        }
        private Result<GetUserReportCommand, Error> CheckUserPermission(GetUserReportCommand command)
        {
            var user = UserRepository.GetByIdAsync(command.AdminUserId).Result;

            if (user.Id != null)
            {
                return user.Role == UserRoleEnum.Manager ? command : new UserUnathorizedError(user.Id);
            }

            return new UserNotFoundError();

        }
        private Result<GetUserReportResponse, Error> GenerateUserReportInTheLastThirtyDays(GetUserReportCommand command)
        {
            var projects = AssignmentLogRepository.GetCompletedAssignmentsByUser(command.UserId, 30).Result;

            var response = new GetUserReportResponse
            {
                UserId = command.UserId,
                CompletedAssignments = projects.Select(x => x.AssignmentState),
            };

            return response;
        }
    }
}

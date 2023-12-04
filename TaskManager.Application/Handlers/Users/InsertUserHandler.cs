using FluentValidation;
using MediatR;
using TaskManager.Application.Commands.Users;
using TaskManager.Application.Responses.Users;
using TaskManager.Application.ResultHandling;
using TaskManager.Application.ResultHandling.Errors;
using TaskManager.Domain.Entities.Users;
using TaskManager.Domain.Repositories.Users;

namespace TaskManager.Application.Handlers.Users
{
    public class InsertUserHandler(IUserRepository userRepository, IValidator<InsertUserCommand> projectValidator)
        : IRequestHandler<InsertUserCommand, Result<InsertUserResponse, Error>>
    {
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IValidator<InsertUserCommand> UserValidator { get; set; } = projectValidator;

        public Task<Result<InsertUserResponse, Error>> Handle(InsertUserCommand command, CancellationToken cancellationToken)
        {
            var response = ValidateRequest(command)
               .Bind(CreateAndInsertUser);

            return Task.FromResult(response);
        }

        private Result<InsertUserCommand, Error> ValidateRequest(InsertUserCommand request)
        {
            var validationResult = UserValidator.ValidateAsync(request).Result;

            if (!validationResult.IsValid)
            {
                return new RequestValidationError(validationResult.Errors);
            }

            return request;
        }
        public Result<InsertUserResponse, Error> CreateAndInsertUser(InsertUserCommand command)
        {
            var user = new User()
            {
                Id = string.Empty,
                UserName = command.Request.UserName,
                Role = command.Request.UserRole,
            };

            UserRepository.InsertAsync(user);

            var response = new InsertUserResponse()
            {
                Id = user.Id
            };

            return response;
        }
    }
}
